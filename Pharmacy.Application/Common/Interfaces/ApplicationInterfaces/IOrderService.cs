using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Common.ValueObjects;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IOrderService
    {
        Task CreateOrder(string userId, DeliveryAddress deliveryAddress);

        Task CreateOrderFromPaymentRequest(IEnumerable<PaymentRequest> paymentRequests);

        Task UpdateOrder(string userId, string createdAt, string orderStatus);
        
        IEnumerable<GroupedOrders> GetUserOrders(out int totalOrderCount, PaginationQuery paginationQuery, string userId);
    }
}
