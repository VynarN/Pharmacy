using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entites;

namespace Pharmacy.Infrastructure.Persistence.Configurations
{
    class MedicamentConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.Property(prop => prop.Price).HasColumnType("decimal(7,2)");

            builder.HasOne(med => med.Instruction)
                   .WithOne(instr => instr.Medicament)
                   .HasForeignKey<Instruction>(instr => instr.MedicamentId);
        }
    }
}
