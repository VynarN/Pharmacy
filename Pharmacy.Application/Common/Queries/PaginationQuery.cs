namespace Pharmacy.Application.Common.Queries
{
    public class PaginationQuery
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public PaginationQuery()
        {
            PageNumber = 1;
            PageSize = 20;
        }

        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize > 100 ? 100 : pageSize;
        }
    }
}
