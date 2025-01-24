using HRManagement.DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HRManagement.DataAccess.Extensions
{

    public static class ApplicationDependenciesConfiguration
    {
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
