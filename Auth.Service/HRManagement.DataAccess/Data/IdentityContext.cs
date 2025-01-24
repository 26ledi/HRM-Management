using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.DataAccess.Data
{
    public class IdentityContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public IdentityContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(IdentityContext).Assembly);
        }
    }
}
