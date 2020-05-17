using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Common.Enums;
using Pharmacy.Domain.Common.ValueObjects;
using Pharmacy.Domain.Entites;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IOrderService
    {
        Task CreateOrder(string userId, DeliveryAddress deliveryAddress);

        Task UpdateOrder(OrderStatus orderStatus, int orderId);

        IQueryable<GroupedOrders> GetOrders(PaginationQuery paginationQuery);
        
        IQueryable<GroupedOrders> GetUserOrders(PaginationQuery paginationQuery, string userId);
    }
}
