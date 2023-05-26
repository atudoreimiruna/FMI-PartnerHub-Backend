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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using Licenta.Core.Entities;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;

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

        // TODO: sterge addnwetonsoftjson
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

        services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
        {
            microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientId"];
            microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
        });

        //services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
        //    .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));

        //services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
        //{
        //    options.Authority = Configuration["AzureAd:Authority"];
        //    options.ClientId = Configuration["AzureAd:ClientId"];
        //    options.ClientSecret = Configuration["AzureAd:ClientSecret"];
        //    options.CallbackPath = Configuration["AzureAd:CallbackPath"];
        //    // Other configuration options as needed
        //});


        //services.AddMicrosoftIdentityWebAppAuthentication(Configuration)
        //    .EnableTokenAcquisitionToCallDownstreamApi()
        //    .AddInMemoryTokenCaches();

        //services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //})
        //    .AddJwtBearer(options =>
        //    {
        //        options.Authority = "https://login.microsoftonline.com/20ed8f4e-52f7-4d77-bfc1-862ced77c351/v2.0";
        //        options.Audience = "f969fab1-6d79-42a0-96c0-32eb06cb8d0b";
        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidIssuer = "https://login.microsoftonline.com/20ed8f4e-52f7-4d77-bfc1-862ced77c351/v2.0",
        //            ValidateAudience = true,
        //            ValidAudience = "f969fab1-6d79-42a0-96c0-32eb06cb8d0b",
        //            ValidateLifetime = true
        //        };
        //    });

        //    options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //}
        //)
        //.AddJwtBearer(options =>
        //{
        //    options.Authority = "https://login.microsoftonline.com/20ed8f4e-52f7-4d77-bfc1-862ced77c351/v2.0";
        //    options.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuer = false,
        //        ValidIssuer = "https://login.microsoftonline.com/",
        //        ValidateAudience = false,
        //        ValidAudience = "f969fab1-6d79-42a0-96c0-32eb06cb8d0b",
        //        ValidateIssuerSigningKey = true,
        //        RequireSignedTokens = true,
        //        RequireExpirationTime = true,
        //        IssuerSigningKeys = new List<SecurityKey>
        //        {
        //            new SymmetricSecurityKey(Encoding.ASCII.GetBytes("-KI3Q9nNR7bRofxmeZoXqbHZGew"))
        //        }
        //    };
        //});


        //services.AddAuthentication(
        //    options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    ).AddMicrosoftAccount(microsoftOptions =>
        //    {
        //        microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientId"];
        //    microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
        //});

        //services.AddIdentity<User, Role>(options =>
        //                                  options.SignIn.RequireConfirmedAccount = true)
        //                                 .AddEntityFrameworkStores<AppDbContext>();

        //.AddMicrosoftAccount(microsoftOptions =>
        //{
        //    microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientId"];
        //    microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
        //});

        //services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //}).AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAd"));

        //services.AddMicrosoftIdentityWebApiAuthentication(Configuration.GetSection("AzureAd"));
        //services.AddEndpointsApiExplorer();
        //AzureADDefaults.JwtBearerAuthenticationScheme

        //services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = AzureADDefaults.JwtBearerAuthenticationScheme;
        //    options.DefaultChallengeScheme = AzureADDefaults.JwtBearerAuthenticationScheme;
        //    options.DefaultScheme = AzureADDefaults.JwtBearerAuthenticationScheme;
        //})
        //.AddAzureADBearer(options =>
        //{
        //    options.ClientId = Configuration["AzureAd:ClientId"];
        //    options.Instance = Configuration["AzureAd:Instance"];
        //    options.TenantId = Configuration["AzureAd:TenantId"];
        //});

        //services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
        //{
        //    options.Authority = options.Authority + "/v2.0";
        //    options.TokenValidationParameters.ValidateIssuer = false;
        //});

        services.AddIdentity<User, Role>()
           .AddEntityFrameworkStores<AppDbContext>();
         //.AddDefaultTokenProviders();


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
