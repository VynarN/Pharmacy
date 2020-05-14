using Microsoft.AspNetCore.WebUtilities;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Queries;
using System;

namespace Pharmacy.Application.Services
{
    public class UriService: IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPaginationUri(PaginationQuery paginationQuery, MedicamentFilterQuery medicamentFilterQuery)
        {
            var uri = new Uri(_baseUri);

            if (paginationQuery == null)
            {
                return uri;
            }

            var modifiedUrl = QueryHelpers.AddQueryString(_baseUri, "pageNumber", paginationQuery.PageNumber.ToString());
            modifiedUrl = QueryHelpers.AddQueryString(modifiedUrl, "pageSize", paginationQuery.PageSize.ToString());

            return new Uri(modifiedUrl);
        }
    }
}
