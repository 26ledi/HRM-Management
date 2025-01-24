using HRManagement.Auth.API.Profiles;
using HRManagement.BusinessLogic.Configurations;
using HRManagement.BusinessLogic.Helpers;
using HRManagement.BusinessLogic.Repositories.Implementations;
using HRManagement.BusinessLogic.Repositories.Interfaces;
using HRManagement.BusinessLogic.SeedData;
using HRManagement.BusinessLogic.Services.Implementations;
using HRManagement.DataAccess.Data;
using HRManagement.DataAccess.Extensions;
using HRManagement.DataAccess.SeedData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

namespace HRManagement.Auth.API
{
    public static class ApplicationDependenciesConfiguration
    {
        public static IServiceCollection ConfigureServices(this WebApplicationBuilder builder)
        {
            AddJwtToken(builder);
            AddSwagger(builder);
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            builder.Services.AddIdentityDatabase(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            })
            .AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            })
            .AddAutoMapper(typeof(ApplicationProfile), typeof(MappingProfiles));

            return builder.Services;
        }

        public static IServiceCollection AddJwtToken(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:key"]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true
                };

            });

            return builder.Services;
        }
        public static IServiceCollection AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please Enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "Jwt",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                       {
                             new OpenApiSecurityScheme
                             {
                               Reference = new OpenApiReference
                               {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                               }
                              },
                              new string[] { }
                            }
                      });
            });

            return builder.Services;
        }

        public async static Task UseMigration(this WebApplication application)
        {
            var serviceScopeFactory = application.Services.GetService<IServiceScopeFactory>();
            using var scope = serviceScopeFactory.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<IdentityContext>();
            await handler.Database.MigrateAsync();

            var roleSeed = scope.ServiceProvider.GetRequiredService<SeedRole>();
            await roleSeed.InitializeRolesAsync();

            var adminSeed = scope.ServiceProvider.GetRequiredService<SeedAdmin>();
            await adminSeed.InitializeAdminAsync();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IUserAuthenticationService, UserAuthenticationService>()
                .AddScoped<SeedRole>()
                .AddScoped<SeedAdmin>();
        }

        public static IServiceCollection AddLogger(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName();
            });

            return builder.Services;
        }
    }
}
