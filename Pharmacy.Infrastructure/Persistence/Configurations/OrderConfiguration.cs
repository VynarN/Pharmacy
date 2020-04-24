using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entites;

namespace Pharmacy.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(prop => prop.Total).HasColumnType("decimal(7,2)");

            builder.HasOne(sc => sc.User).WithMany(s => s.Orders)
                                         .HasForeignKey(sc => sc.UserId);

            builder.HasOne(sc => sc.Medicament).WithMany(c => c.Orders)
                                               .HasForeignKey(sc => sc.MedicamentId);
        }
    }
}
