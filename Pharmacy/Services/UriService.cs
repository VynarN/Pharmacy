using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;

namespace Pharmacy.Api.Services
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
            var modifiedUri = QueryHelpers.AddQueryString(_baseUri, "PageNumber", paginationQuery.PageNumber.ToString());
            
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "PageSize", paginationQuery.PageSize.ToString());

            if (!string.IsNullOrEmpty(medicamentFilterQuery.Categories))
                modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "Categories", medicamentFilterQuery.Categories);

            if (!string.IsNullOrEmpty(medicamentFilterQuery.ApplicationMethods))
                modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "ApplicationMethods", medicamentFilterQuery.ApplicationMethods);

            if (!string.IsNullOrEmpty(medicamentFilterQuery.MedicamentForms))
                 modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "MedicamentForms", medicamentFilterQuery.MedicamentForms);

            if (!string.IsNullOrEmpty(medicamentFilterQuery.AllowedFor))
                modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "AllowedFor", medicamentFilterQuery.AllowedFor);

            return modifiedUri;
        }

        public string GetPaginationUri(PaginationQuery paginationQuery)
        {
            var modifiedUri = QueryHelpers.AddQueryString(_baseUri, "PageNumber", paginationQuery.PageNumber.ToString());

            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "PageSize", paginationQuery.PageSize.ToString());

            return modifiedUri;
        }
    }
}
