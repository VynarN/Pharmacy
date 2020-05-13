using System.Collections.Generic;

namespace Pharmacy.Application.Common.AppObjects
{
    public class PaginatedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public double TotalPages { get; set; }

        public string NextPage { get; set; }

        public string PreviousPage { get; set; }

        public PaginatedResponse() { }

        public PaginatedResponse(IEnumerable<T> data)
        {
            Data = data;
        }
    }
}
