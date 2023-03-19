using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Licenta.Infrastructure;
using Licenta.Services.Interfaces;
using Licenta.Services.Managers;
using Licenta.Services.Helpers;
using Licenta.Infrastructure.Seeders;
using Licenta.Core.Entities;
using AutoMapper;
using Licenta.Services.AutoMapper;
using System.Data;

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
                                  builder.WithOrigins("localhost:4200", "http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                              });
        });

        services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnString")));
        services.AddTransient<IAuthManager, AuthManager>();
        services.AddTransient<ITokenHelper, TokenHelper>();
        services.AddTransient<DataSeeder>();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Licenta", Version = "v1" });
        });

        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("AuthScheme", options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                var secret = Configuration.GetSection("Jwt").GetSection("Token").Get<String>();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
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


        services.AddAuthorization(opt =>
        {
            opt.AddPolicy("Admin", policy => policy.RequireRole("Admin").RequireAuthenticatedUser().AddAuthenticationSchemes("AuthScheme").Build());
            opt.AddPolicy("User", policy => policy.RequireRole("User").RequireAuthenticatedUser().AddAuthenticationSchemes("AuthScheme").Build());
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
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataSeeder initialSeed)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Licenta v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(SpecificOrigins);

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        initialSeed.CreateRoles();
    }
}
