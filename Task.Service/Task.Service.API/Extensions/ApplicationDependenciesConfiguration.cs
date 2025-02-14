using HRManagement.MessageBrokers.Shared;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Task.Service.API.Configurations;
using Task.Service.API.Consumers;

namespace Task.Service.API.Extensions
{
    public static class ApplicationDependenciesConfiguration
    {
        public static IServiceCollection AddConfigurations(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            });

            return builder.Services;
        }
        public static void ConfigureMassTransit(this WebApplicationBuilder builder)
        {
            builder.Services.AddOptions<RabbitMQConfiguration>().Bind(builder.Configuration.GetSection("RabbitMQ"));

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<UserRegisteredConsumer>();
                x.AddConsumer<UserUpdatedConsumer>();
                x.AddConsumer<UserDeletedConsumer>();
                
                x.UsingRabbitMq((context, cfg) =>
                {
                    var options = context.GetRequiredService<IOptions<RabbitMQConfiguration>>().Value;

                    cfg.Host(options.Host, options.VirtualHost, h =>
                    {
                        h.Username(options.Username);
                        h.Password(options.Password);
                    });

                    cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(true));

                    cfg.ReceiveEndpoint("HRManagement.TaskService.Registration", c =>
                    {
                        c.ConfigureConsumer<UserRegisteredConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("HRManagement.TaskService.Deleting", c =>
                    {
                        c.ConfigureConsumer<UserDeletedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("HRManagement.TaskService.Updating", c =>
                    {
                        c.ConfigureConsumer<UserUpdatedConsumer>(context);
                    });
                });
            });
        }

        public static void ConfigureCrossOriginRessourceSharing(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
        }
    }
}
