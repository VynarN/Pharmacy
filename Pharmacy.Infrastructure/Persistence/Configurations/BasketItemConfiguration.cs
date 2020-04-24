using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entites;

namespace Pharmacy.Infrastructure.Persistence.Configurations
{
    class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.HasKey(prop => new { prop.UserId, prop.MedicamentId });
        }
    }
}
