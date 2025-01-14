using Microsoft.AspNetCore.Identity;

namespace HRManagement.BusinessLogic.SeedData
{
    public class SeedRole
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeedRole"/> class.
        /// </summary>
        /// <param name="roleManager">The role manager.</param>
        public SeedRole(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        /// <summary>
        /// Initializes the predefined roles in the application if they do not already exist.
        /// </summary>
        public async Task InitializeRolesAsync()
        {
            string[] roleNames = { "Admin", "Teacher"};
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
