using Application.Implementations;
using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories.Implementations;
using Persistence.Repositories.Interfaces;
using Services.Abstractions.Repositories;
using Services.Abstractions.Services;

namespace DependancyInjection
{
    public static class DependacyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Register Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskEvaluationRepository, TaskEvaluationRepository>();
            services.AddScoped<IUserTaskRepository, UserTaskRepository>();

            //Register Service
            services.AddScoped<IUserTaskService, UserTaskService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskEvaluationService, TaskEvaluationService>();

            return services;
        }
    }
}
