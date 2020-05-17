using Pharmacy.Application.Common.Queries;

namespace Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces
{
    public interface IUriService
    {
        string GetMedicamentsPaginationUri(PaginationQuery paginationQuery,  MedicamentFilterQuery medicamentFilterQuery );
    }
}
