using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Common.Enums;
using Pharmacy.Domain.Entites;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IOrderService
    {
        Task CreateOrders(int userId);

        Task UpdateOrder(OrderStatus orderStatus, int orderId);

        IQueryable<Order> GetOrders(PaginationQuery paginationQuery);
        
        IQueryable<Order> GetUserOrders(PaginationQuery paginationQuery, int userId);
    }
}
