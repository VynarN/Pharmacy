using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pharmacy.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
            new IdentityRole
            {
                Id = "1",
                Name = "mainadmin",
                NormalizedName = "MAINADMIN"
            },
            new IdentityRole
            {
                Id = "2",
                Name = "admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Id = "3",
                Name = "manager",
                NormalizedName = "MANAGER"
            },
            new IdentityRole
            {
                Id = "4",
                Name = "user",
                NormalizedName = "USER"
            });

        }
    }
}
