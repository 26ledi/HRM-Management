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
            var adminUser = await _userManager.FindByNameAsync(username);

            if (adminUser == null)
            {
                adminUser = new IdentityUser { UserName = username, Email = email, EmailConfirmed = true, Id = Guid.NewGuid().ToString() };
                var result = await _userManager.CreateAsync(adminUser, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
