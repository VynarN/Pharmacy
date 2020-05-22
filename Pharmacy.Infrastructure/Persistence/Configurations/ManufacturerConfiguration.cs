using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entites;
namespace Pharmacy.Infrastructure.Persistence.Configurations
{
    public class ManufacturerConfiguration: IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasOne(mfcr => mfcr.Address)
                   .WithOne(adrs => adrs.Manufacturer)
                   .HasForeignKey<Address>(adrs => adrs.ManufacturerId);

            builder.HasIndex(mfcr => mfcr.PhoneNumber).IsUnique();
            builder.HasIndex(mfcr => mfcr.WebSite).IsUnique();
        }
    }
}
