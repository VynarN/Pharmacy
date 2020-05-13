using Pharmacy.Application.Common.Queries;
using System;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IUriService
    {
        Uri GetPaginationUri(PaginationQuery paginationQuery, MedicamentFilterQuery medicamentFilterQuery );
    }
}
