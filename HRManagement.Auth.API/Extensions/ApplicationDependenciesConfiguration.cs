using HRManagement.BusinessLogic.SeedData;
using HRManagement.DataAccess.Data;
using HRManagement.DataAccess.Extensions;
using HRManagement.DataAccess.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace HRManagement.Auth.API
{
    public static class ApplicationDependenciesConfiguration
    {
        public static IServiceCollection ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentityDatabase(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            })
            .AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please Enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "Jwt",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                      {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new List<string>()
                            }
                      });
            })
            .AddCors(options =>
            {
                options.AddPolicy("MyPolicy", opt =>
                {
                    opt.AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowAnyMethod();
                });
            });

            return builder.Services;
        }
        /// <summary>
        /// Add the migration to the database
        /// </summary>
        /// <param name="application">The application builder</param>
        /// <returns>A <see cref="Task"/></returns>
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
    }
}
