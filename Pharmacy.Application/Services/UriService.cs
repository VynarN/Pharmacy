using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Queries;

namespace Pharmacy.Application.Services
{
    public class UriService: IUriService
    {
        private readonly string _baseUri;

        public UriService(IHttpContextAccessor httpContextAccessor)
        {
            _baseUri = string.Concat(httpContextAccessor.HttpContext.Request.Scheme, "://",
                                     httpContextAccessor.HttpContext.Request.Host.ToUriComponent(),
                                     httpContextAccessor.HttpContext.Request.Path);
        }

        public string GetMedicamentsPaginationUri(PaginationQuery paginationQuery, MedicamentFilterQuery medicamentFilterQuery)
        {
            var modifiedUri = QueryHelpers.AddQueryString(_baseUri, "pageNumber", paginationQuery.PageNumber.ToString());

            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", paginationQuery.PageSize.ToString());

            return modifiedUri;
        }
    }
}
