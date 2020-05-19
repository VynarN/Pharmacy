using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Common.Enums;
using Pharmacy.Domain.Common.ValueObjects;
using Pharmacy.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IOrderService
    {
        Task CreateOrders(string userId, DeliveryAddress deliveryAddress);

        Task CreateOrders(string userId, IEnumerable<PaymentRequest> paymentRequests);

        Task UpdateOrders(string userId, DateTime orderDateTime, OrderStatus orderStatus);

        IQueryable<GroupedUserOrders> GetOrders(PaginationQuery paginationQuery);
        
        GroupedUserOrders GetUserOrders(PaginationQuery paginationQuery, string userId);
    }
}
