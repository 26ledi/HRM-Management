using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRManagement.DataAccess.Configurations
{
    public class IdentityUserConfiguration:IEntityTypeConfiguration<IdentityUser>
    {
        /// <summary>
        /// Configure the user entity
        /// </summary>
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            /// <summary>
            /// <inheritdoc/>
            /// </summary>
            /// <param name="builder">The builder</param>
            builder.HasKey(x => x.Id);
            builder.ToTable("Users");
        }
    }
}
