using HRManagement.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HRManagement.DataAccess.Extensions
{
    // <summary>
    /// The Application configuration to configure the service of the database comming from the Data access
    /// </summary>
    /// <summary>
    public static class ApplicationDependenciesConfiguration
    {
        /// <summary>
        /// Add the identity service database and all the dependencies
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="options">The options builder to be passed to the function</param>
        /// <returns>A <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddIdentityDatabase(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {

            services
                .AddDbContextPool<IdentityContext>(options)
                .AddIdentity<IdentityUser, IdentityRole>(optionsIdentity =>
                {
                    optionsIdentity.User.RequireUniqueEmail = true;
                    optionsIdentity.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+& ";
                    optionsIdentity.Password.RequireNonAlphanumeric = false;
                    optionsIdentity.Password.RequireLowercase = false;
                    optionsIdentity.Password.RequireUppercase = false;
                    optionsIdentity.Password.RequireDigit = false;
                    optionsIdentity.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                    optionsIdentity.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
