using Microsoft.AspNetCore.Identity;

namespace HRManagement.BusinessLogic.Helpers
{
    public static class UserHelper
    {
        /// <summary>
        /// Adds a role to a user asynchronously and throws an exception if the operation fails, logging errors if present.
        /// </summary>
        /// <typeparam name="T">Type of the user (should be derived from IdentityUser).</typeparam>
        /// <param name="userManager">The UserManager used to manage users.</param>
        /// <param name="role">The role to add to the user.</param>
        /// <param name="user">The user to whom the role will be added.</param>
        /// <returns>The user after the role is added.</returns>
        public async static Task<T> AddRoleToUserAsync<T>(this UserManager<T> userManager, string role, T user) where T : IdentityUser
        {
            var roleResult = await userManager.AddToRoleAsync(user, role);

            return user;
        }

        /// <summary>
        /// Checks if a user with the given email exists.
        /// </summary>
        /// <typeparam name="T">Type of the user (should be derived from IdentityUser).</typeparam>
        /// <param name="userManager">The UserManager used to manage users.</param>
        /// <param name="email">The email to check for existence.</param>
        /// <returns>True if a user with the given email exists, otherwise false.</returns>
        public async static Task<bool> IsUserEmailExist<T>(this UserManager<T> userManager, string email) where T : IdentityUser
        {
            var userLooked = await userManager.FindByEmailAsync(email);

            return userLooked is not null;
        }
    }
}
