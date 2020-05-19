using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Entites;
using System.Reflection;

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
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentResponse> CommentResponses { get; set; }

        public PharmacyContext(DbContextOptions<PharmacyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
