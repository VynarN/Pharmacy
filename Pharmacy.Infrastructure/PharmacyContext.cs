using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Entites;

namespace Pharmacy.Infrastructure
{
    public class PharmacyContext : IdentityDbContext<User>
    {
        public DbSet<AllowedForEntity> AllowedForEntities { get; set; }

        public DbSet<ApplicationMethod> ApplicationMethods { get; set; }

        public DbSet<BasketItem> BasketItems { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<MedicamentForm> MedicamentForms { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Instruction> Instructions { get; set; }

        public PharmacyContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BasketItem>().HasKey(prop => new { prop.UserId, prop.MedicamentId });

            modelBuilder.Entity<Order>().Property(prop => prop.Total).HasColumnType("decimal(7,2)");
            modelBuilder.Entity<Medicament>().Property(prop => prop.Price).HasColumnType("decimal(7,2)");

            modelBuilder.Entity<Order>().HasOne(sc => sc.User)
                                        .WithMany(s => s.Orders)
                                        .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<Order>().HasOne(sc => sc.Medicament)
                                        .WithMany(c => c.Orders)
                                        .HasForeignKey(sc => sc.MedicamentId);
        }
    }
}
