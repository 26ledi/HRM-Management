using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.DataAccess.Data
{
    /// <summary>
    /// The Identity context
    /// </summary>
    public class IdentityContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IdentityContext"/>
        /// </summary>
        /// <param name="options">Options for the db context</param>
        public IdentityContext(DbContextOptions options) : base(options)
        {
            Database.EnsureDeleted();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="builder">The builder</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(IdentityContext).Assembly);
        }
    }
}
