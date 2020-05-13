using Microsoft.AspNetCore.WebUtilities;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Queries;
using System;

namespace Pharmacy.Application.Services
{
    public class UriService: IUriService
    {
        private string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPaginationUri(PaginationQuery pagination, MedicamentFilterQuery query)
        {
            var uri = new Uri(_baseUri);

            if (pagination == null)
            {
                return uri;
            }

            var modifiedUrl = QueryHelpers.AddQueryString(_baseUri, "pageNumber", pagination.PageNumber.ToString());
            modifiedUrl = QueryHelpers.AddQueryString(modifiedUrl, "pageSize", pagination.PageSize.ToString());

            return new Uri(modifiedUrl);
        }
    }
}
