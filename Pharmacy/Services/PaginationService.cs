using Pharmacy.Application.Common.AppObjects;
using Pharmacy.Application.Common.DTO.Out;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
using System.Collections.Generic;

namespace Pharmacy.Api.Services
{
    public class PaginationService: IPaginationService
    {
        private readonly IUriService _uriService;

        public PaginationService(IUriService uriService)
        {
            _uriService = uriService;
        }

        public PaginatedResponse<MedicamentOutDto> FormMedicamentsPaginatedResponse(int medicamentsCount,
                                                                         IEnumerable<MedicamentOutDto> medicamentsDto,
                                                                         PaginationQuery paginationQuery, 
                                                                         MedicamentFilterQuery medicamentFilterQuery)
        {
            var paginatedResponse = new PaginatedResponse<MedicamentOutDto>(medicamentsDto);

            paginatedResponse.PageNumber = paginationQuery.PageNumber;
            paginatedResponse.PageSize = paginationQuery.PageSize;

            paginatedResponse.TotalPages = GetTotalPages(medicamentsCount, paginationQuery.PageSize);
           

            paginatedResponse.NextPage = paginatedResponse.TotalPages > paginationQuery.PageNumber ?
                                        _uriService.GetMedicamentsPaginationUri(
                                                new PaginationQuery()
                                                {
                                                    PageNumber = paginationQuery.PageNumber + 1,
                                                    PageSize = paginationQuery.PageSize
                                                },
                                                medicamentFilterQuery) : null;

           paginatedResponse.PreviousPage = paginatedResponse.PageNumber > 1 ?
                                            _uriService.GetMedicamentsPaginationUri(
                                                    new PaginationQuery()
                                                    {
                                                        PageNumber = paginationQuery.PageNumber - 1,
                                                        PageSize = paginationQuery.PageSize
                                                    },
                                                    medicamentFilterQuery) : null;
            return paginatedResponse;
        }

        public int GetTotalPages(int entitiesCount, int pageSize)
        {
            return (entitiesCount % pageSize) != 0 ?
                   (entitiesCount / pageSize + 1) :
                   (entitiesCount / pageSize);
        }
    }
}
