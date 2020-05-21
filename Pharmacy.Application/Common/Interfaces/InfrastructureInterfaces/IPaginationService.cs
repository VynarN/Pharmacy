using Pharmacy.Application.Common.AppObjects;
using Pharmacy.Application.Common.DTO;
using Pharmacy.Application.Common.Queries;
using System.Collections.Generic;

namespace Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces
{
    public interface IPaginationService
    {
        PaginatedResponse<MedicamentBaseInfoDto> FormMedicamentsPaginatedResponse(int medicamentsCount, 
                                                   IEnumerable<MedicamentBaseInfoDto> medicamentsDto,
                                                   PaginationQuery paginationQuery, 
                                                   MedicamentFilterQuery medicamentFilterQuery);

        PaginatedResponse<T> FormPaginatedResponse<T>(int totalCount, IEnumerable<T> dtos, PaginationQuery paginationQuery);
    }
}

