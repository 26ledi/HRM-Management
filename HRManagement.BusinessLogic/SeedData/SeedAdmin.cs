using Microsoft.AspNetCore.Identity;
using System.Web.Providers.Entities;

namespace HRManagement.DataAccess.SeedData
{
    public class SeedAdmin
    {
        private readonly UserManager<IdentityUser> _userManager;

        public  SeedAdmin(UserManager<IdentityUser> userManager) 
        {
            _userManager = userManager;
        }

        public async Task InitializeAdminAsync()
        {
            string username = "joyceledi";
            string email = "joyceledi26@gmail.com";
            string password = "Gir@ffe21$N3t";
            var managerUser = await _userManager.FindByNameAsync(username);

            if (managerUser == null)
            {
                managerUser = new IdentityUser { UserName = username, Email = email, EmailConfirmed = true, Id = Guid.NewGuid().ToString() };
                var result = await _userManager.CreateAsync(managerUser, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(managerUser, "Manager");
                }
            }
        }
    }
}
