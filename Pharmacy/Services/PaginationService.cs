using Pharmacy.Application.Common.AppObjects;
using Pharmacy.Application.Common.DTO;
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

        public PaginatedResponse<MedicamentBaseInfoDto> FormMedicamentsPaginatedResponse(int medicamentsCount,
                                                                         IEnumerable<MedicamentBaseInfoDto> medicamentsDto,
                                                                         PaginationQuery paginationQuery, 
                                                                         MedicamentFilterQuery medicamentFilterQuery)
        {
            var paginatedResponse = new PaginatedResponse<MedicamentBaseInfoDto>(medicamentsDto);

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

        public PaginatedResponse<T> FormPaginatedResponse<T>(int totalCount, IEnumerable<T> dtos, PaginationQuery paginationQuery)
        {
            var paginatedResponse = new PaginatedResponse<T>(dtos);

            paginatedResponse.PageNumber = paginationQuery.PageNumber;
            paginatedResponse.PageSize = paginationQuery.PageSize;

            paginatedResponse.TotalPages = GetTotalPages(totalCount, paginationQuery.PageSize);


            paginatedResponse.NextPage = paginatedResponse.TotalPages > paginationQuery.PageNumber ?
                                        _uriService.GetPaginationUri(
                                                new PaginationQuery()
                                                {
                                                    PageNumber = paginationQuery.PageNumber + 1,
                                                    PageSize = paginationQuery.PageSize
                                                }) : null;

            paginatedResponse.PreviousPage = paginatedResponse.PageNumber > 1 ?
                                             _uriService.GetPaginationUri(
                                                     new PaginationQuery()
                                                     {
                                                         PageNumber = paginationQuery.PageNumber - 1,
                                                         PageSize = paginationQuery.PageSize
                                                     }) : null;
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
