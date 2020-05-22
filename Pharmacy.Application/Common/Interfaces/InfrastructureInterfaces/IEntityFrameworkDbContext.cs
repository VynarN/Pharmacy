namespace Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces
{
    public interface IEntityFrameworkDbContext
    {
        void DetachAllEntities();
    }
}
