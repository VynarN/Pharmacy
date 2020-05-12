using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entites;

namespace Pharmacy.Infrastructure.Persistence.Configurations
{
    public class PaymentRequestConfiguration: IEntityTypeConfiguration<PaymentRequest>
    {
        public void Configure(EntityTypeBuilder<PaymentRequest> builder)
        {
            builder.Property(prop => prop.Total).HasColumnType("decimal(7,2)");

            builder.HasOne(pr => pr.Sender).WithMany(us => us.PaymentRequests)
                                         .HasForeignKey(pr => pr.SenderId);

            builder.HasOne(pr => pr.Medicament).WithMany(m => m.PaymentRequests)
                                               .HasForeignKey(sc => sc.MedicamentId);
        }
    }
}
