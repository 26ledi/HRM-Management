using HRManagement.MessageBrokers.Shared;
using MassTransit;
using Microsoft.Extensions.Options;
using Notification.Service.Configurations;
using Notification.Service.Consumers;
using Notification.Service.Profiles;
using Notification.Service.Services;

namespace Notification.Service.Extensions
{
    public static class ApplicationDependenciesConfiguration
    {

        public static IServiceCollection AddConfigurations(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSetting"));
            builder.Services.Configure<WelcomeEmailSettings>(builder.Configuration.GetSection("WelcomeEmail"));
            builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

            return builder.Services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }

        public static void ConfigureMassTransit(this WebApplicationBuilder builder)
        {
            builder.Services.AddOptions<RabbitMQConfiguration>().Bind(builder.Configuration.GetSection("RabbitMQ"));

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<UserRegisteredConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    var options = context.GetRequiredService<IOptions<RabbitMQConfiguration>>().Value;

                    cfg.Host(options.Host, options.VirtualHost, h =>
                    {
                        h.Username(options.Username);
                        h.Password(options.Password);
                    });

                    cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(true));

                    cfg.ReceiveEndpoint("HRManagement.Registration", c =>
                    {
                        c.ConfigureConsumer<UserRegisteredConsumer>(context);
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

        public static IServiceCollection AddMapperServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));

            return services;
        }
    }
}
