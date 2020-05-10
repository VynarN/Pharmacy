using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Entites;
using Pharmacy.Infrastructure.Persistence.Configurations;

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

        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }

        public DbSet<PaymentRequest> PaymentRequests { get; set; }

        public DbSet<Instruction> Instructions { get; set; }

        public PharmacyContext(DbContextOptions<PharmacyContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new MedicamentConfiguration());
            modelBuilder.ApplyConfiguration(new BasketItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
        }
    }
}
