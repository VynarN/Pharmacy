using Pharmacy.Application.Common.Queries;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IUriService
    {
        string GetMedicamentsPaginationUri(PaginationQuery paginationQuery,  MedicamentFilterQuery medicamentFilterQuery );
    }
}
