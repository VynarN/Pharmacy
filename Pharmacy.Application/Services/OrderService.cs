using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Common.Enums;
using Pharmacy.Domain.Common.ValueObjects;
using Pharmacy.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<BasketItem> _basketItemsRepo;
        private readonly IDeliveryAddressService _deliveryAddressService;

        public OrderService(IRepository<Order> orderRepository, IRepository<BasketItem> basketItemRepo, IDeliveryAddressService deliveryAddressService)
        {
            _orderRepository = orderRepository;
            _basketItemsRepo = basketItemRepo;
            _deliveryAddressService = deliveryAddressService;
        }

        public async Task CreateOrders(string userId, DeliveryAddress deliveryAddress)
        {
            var userBasketItems = _basketItemsRepo.GetWithInclude(bi => bi.UserId.Equals(userId), bi => bi.Medicament);

            var addressId = deliveryAddress.Id != 0 ? deliveryAddress.Id : await _deliveryAddressService.CreateDeliveryAddress(deliveryAddress);

            var orders = userBasketItems.Select(basketItem => new Order()
            {
                UserId = userId,
                DeliveryAddressId = addressId,
                MedicamentId = basketItem.MedicamentId,
                Quantity = basketItem.ProductQuantity,
                Total = basketItem.Medicament.Price * basketItem.ProductQuantity,
                Status = OrderStatus.Pending
            });

            await _orderRepository.Create(orders);

            await _basketItemsRepo.Delete(userBasketItems);
        }


        public Task CreateOrders(string userId, IEnumerable<PaymentRequest> paymentRequests)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrders(string userId, DateTime orderDateTime, OrderStatus orderStatus)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<GroupedUserOrders> GetOrders(PaginationQuery paginationQuery)
        {
            return _orderRepository.GetWithInclude(obj => obj.DeliveryAddress, obj => obj.Medicament)
                                   .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                                   .Take(paginationQuery.PageSize)
                                   .AsEnumerable()
                                   .GroupBy(order => order.UserId)
                                   .Select(g => new GroupedUserOrders() {
                                       UserId = g.Key,
                                       GroupedOrders = g.Select(order => order)
                                                        .GroupBy(order => new
                                                        {
                                                            Created = order.CreatedAt.ToString(StringConstants.DateTimeFormat),
                                                            Dispatched = order.DispatchedAt?.ToString(StringConstants.DateTimeFormat),
                                                            Delivered = order.DeliveredAt?.ToString(StringConstants.DateTimeFormat),
                                                            Status = order.Status.ToString()
                                                        })
                                                        .Select(g => new GroupedOrders() {
                                                            CreatedAt = g.Key.Created,
                                                            DispatchedAt = g.Key.Dispatched,
                                                            DeliveredAt = g.Key.Delivered,
                                                            Status = g.Key.Status,
                                                            Orders = g.Select(order => order),
                                                            OrdersTotal = g.Sum(order => order.Total) })
                                   }).AsQueryable();
        }

        public GroupedUserOrders GetUserOrders(PaginationQuery paginationQuery, string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
