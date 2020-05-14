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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new MedicamentConfiguration());
            builder.ApplyConfiguration(new BasketItemConfiguration());
            builder.ApplyConfiguration(new PaymentRequestConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new ManufacturerConfiguration());
        }
    }
}
