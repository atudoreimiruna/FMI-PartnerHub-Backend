using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Licenta.Infrastructure;
using Licenta.Infrastructure.Seeders;
using AutoMapper;
using Licenta.Services.AutoMapper;
using System.Collections.Generic;
using Licenta.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Web;
using Hangfire;
using Hangfire.MySql;
using Licenta.Services.Interfaces.External;
using Licenta.External.Hangfire;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.Threading.Tasks;
using Licenta.External.Authorization;

namespace Licenta.Api;

public class Startup
{
    public string SpecificOrigins = "_allowSpecificOrigins";

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {

        services.AddCors(options =>
        {
            options.AddPolicy(name: SpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
        });

        services
            .AddControllers()
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        string _connString = Configuration.GetConnectionString("ConnString");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(_connString, ServerVersion.AutoDetect(_connString));
        });

        services.AddServices();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Licenta", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });
        });

        services.AddAuthentication()
            .AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientId"]!;
                microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"]!;
            })
            .AddMicrosoftIdentityWebApi(options =>
            {
                options.Audience = Configuration["AzureAd:Audience"];
                options.Authority = Configuration["AzureAd:Authority"];
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            }, identityOptions =>
            {
                identityOptions.Instance = Configuration["AzureAd:Instance"]!;
                identityOptions.ClientId = Configuration["AzureAd:ClientId"];
                identityOptions.TenantId = Configuration["AzureAd:TenantId"];
            },
            Constants.AzureAd);

        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthorization(opt =>
        {
            opt.AddPolicy(AuthPolicy.SuperAdmin, policy => policy.RequireRole(AuthPolicy.SuperAdmin));
            opt.AddPolicy(AuthPolicy.Admin, policy => policy.RequireRole(AuthPolicy.Admin));
            opt.AddPolicy(AuthPolicy.User, policy => policy.RequireRole(AuthPolicy.User));
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            //var secret = Configuration.GetSection("Jwt").GetSection("Token").Get<String>();
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                //ValidateIssuerSigningKey = true,
                //ValidateLifetime = true,
                //RequireExpirationTime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456789012345689999")),
            };
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                    }
                    return Task.CompletedTask;
                }
            };
        });

        var mapperConfig = new MapperConfiguration(options =>
        {
            var namingConvention = new ExactMatchNamingConvention();
            options.SourceMemberNamingConvention = namingConvention;
            options.DestinationMemberNamingConvention = namingConvention;
            options.AllowNullCollections = true;
            options.AllowNullDestinationValues = true;

            options.AddProfile<MappingProfile>();
        });
        mapperConfig.AssertConfigurationIsValid();
        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddHangfire(options =>
        {
            options.UseStorage(new MySqlStorage(Configuration.GetConnectionString("ConnString"), new MySqlStorageOptions
            {
                TablesPrefix = "__hangfire.",
            }));
        });
        services.AddScoped<IHangfireManager, HangfireManager>();

        services.AddHangfireServer();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataSeeder initialSeed, IModelService modelService)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Licenta v1"));

            app.UseHangfireDashboard("/hangfire");
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(SpecificOrigins);

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        initialSeed.CreateRoles();

         // modelService.RunModelAsync();

        RecurringJob.AddOrUpdate<IHangfireManager>(
            "Run once a month",
            hangfireManager => hangfireManager.SendMonthlyEmail(),
            "0 9 1 * *");

        RecurringJob.AddOrUpdate<IModelService>(
            "Run once a day",
            modelService => modelService.RunModelAsync(),
            "0 1 * * *");
    }
}
