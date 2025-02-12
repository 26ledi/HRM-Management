using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;

namespace Configurations
{
    public static class DatabaseConfigurations
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Database")));
        }
    }
}
