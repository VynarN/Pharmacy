using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Common.ValueObjects;
using Pharmacy.Domain.Entites;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

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

        private readonly ICurrentUser _currentUser;

        public PharmacyContext(DbContextOptions<PharmacyContext> options, ICurrentUser currentUser) : base(options) 
        {
            _currentUser = currentUser;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUser.UserId;
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = _currentUser.UserId;
                        entry.Entity.ModifiedAt = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
