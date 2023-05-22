using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Licenta.Infrastructure;
using Licenta.Infrastructure.Seeders;
using Licenta.Core.Entities;
using AutoMapper;
using Licenta.Services.AutoMapper;
using System;

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

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Licenta", Version = "v1" });
        });

        services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication()
            .AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientId"];
                microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
            });

        //services
        //    .AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer("AuthScheme", options =>
        //    {
        //        options.RequireHttpsMetadata = true;
        //        options.SaveToken = true;
        //        var secret = Configuration.GetSection("Jwt").GetSection("Token").Get<String>();
        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            ValidateLifetime = true,
        //            RequireExpirationTime = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
        //            ValidateIssuer = false,
        //            ValidateAudience = false
        //        };
        //        options.Events = new JwtBearerEvents
        //        {
        //            OnAuthenticationFailed = context =>
        //            {
        //                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
        //                {
        //                    context.Response.Headers.Add("Token-Expired", "true");
        //                }
        //                return Task.CompletedTask;
        //            }
        //        };
        //    });


        //services.AddAuthorization(opt =>
        //{
        //    opt.AddPolicy("Admin", policy => policy.RequireRole("Admin").RequireAuthenticatedUser().AddAuthenticationSchemes("AuthScheme").Build());
        //    opt.AddPolicy("User", policy => policy.RequireRole("User").RequireAuthenticatedUser().AddAuthenticationSchemes("AuthScheme").Build());
        //});

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

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        initialSeed.CreateRoles();
    }
}
