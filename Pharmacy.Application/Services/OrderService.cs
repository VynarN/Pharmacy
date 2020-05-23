using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Common.Enums;
using Pharmacy.Domain.Common.Exceptions;
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
        private readonly IRepository<Medicament> _medicamentRepo;
        private readonly IDeliveryAddressService _deliveryAddressService;

        public OrderService(IRepository<Order> orderRepository, IRepository<BasketItem> basketItemRepo, IRepository<Medicament> medicamentRepo, 
                            IDeliveryAddressService deliveryAddressService)
        {
            _orderRepository = orderRepository;
            _basketItemsRepo = basketItemRepo;
            _medicamentRepo = medicamentRepo;
            _deliveryAddressService = deliveryAddressService;
        }

        public async Task CreateOrder(string userId, DeliveryAddress deliveryAddress)
        {
            var userBasketItems = _basketItemsRepo.GetWithInclude(bi => bi.UserId.Equals(userId), bi => bi.Medicament).AsEnumerable();

            var addressId = deliveryAddress.Id != 0 ? deliveryAddress.Id : await _deliveryAddressService.CreateDeliveryAddress(deliveryAddress);

            var ordersAndMedicaments = userBasketItems.Select(basketItem => new
            {
                Order = new Order()
                {
                    UserId = userId,
                    DeliveryAddressId = addressId,
                    MedicamentId = basketItem.MedicamentId,
                    Quantity = basketItem.Medicament.QuantityInStock >= basketItem.ProductQuantity
                             ? basketItem.ProductQuantity
                             : throw new ProductException(string.Format(ModelValidationStrings.ProductQuantity,
                                                                 basketItem.Medicament.Name, basketItem.Medicament.QuantityInStock)),
                    Total = basketItem.Medicament.Price * basketItem.ProductQuantity,
                    Status = OrderStatus.Pending
                },
                basketItem.Medicament
            });

            var medicaments = new List<Medicament>();

            foreach(var orderAndMedicament in ordersAndMedicaments)
            {
                orderAndMedicament.Medicament.Offtake += orderAndMedicament.Order.Quantity;
                orderAndMedicament.Medicament.QuantityInStock -= orderAndMedicament.Order.Quantity;
                medicaments.Add(orderAndMedicament.Medicament);
            }

            var orders = ordersAndMedicaments.Select(om => om.Order);

            await _orderRepository.Create(orders);

            await _medicamentRepo.Update(medicaments);

        }

        public async Task CreateOrderFromPaymentRequest(IEnumerable<PaymentRequest> paymentRequests)
        {
            var orders = paymentRequests.Select(pr => new Order()
            {
                DeliveryAddressId = pr.DeliveryAddressId,
                MedicamentId = pr.MedicamentId,
                UserId = pr.SenderId,
                Quantity = pr.Quantity,
                Total = pr.Total,
                Status = OrderStatus.Pending
            });

            await _orderRepository.Create(orders);
        }

        public async Task UpdateOrder(string userId, string createdAt, string orderStatus)
        {
            var userOrders = _orderRepository.GetByPredicate(order => order.UserId.Equals(userId))
                                             .AsEnumerable()
                                             .Where(order => order.CreatedAt.ToString(StringConstants.DateTimeFormat).Equals(createdAt));

            foreach (var order in userOrders)
            {
                order.Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), orderStatus);

                if (order.Status == OrderStatus.Dispatched)
                    order.DispatchedAt = DateTime.Now;
                else if (order.Status == OrderStatus.Delivered)
                    order.DeliveredAt = DateTime.Now;
            }

            await _orderRepository.Update(userOrders);
        }

        public IEnumerable<GroupedOrders> GetUserOrders(out int totalOrderCount, PaginationQuery paginationQuery, string userId)
        {
            var groupedOrders = _orderRepository.GetWithInclude(obj => obj.DeliveryAddress, obj => obj.Medicament)
                                   .Where(order => order.UserId.Equals(userId))
                                   .AsEnumerable()
                                   .GroupBy(order => new
                                   {
                                       Created = order.CreatedAt.ToString(StringConstants.DateTimeFormat),
                                       Dispatched = order.DispatchedAt?.ToString(StringConstants.DateTimeFormat),
                                       Delivered = order.DeliveredAt?.ToString(StringConstants.DateTimeFormat),
                                       Status = order.Status.ToString()
                                   });

            totalOrderCount = groupedOrders.Count();

            return  groupedOrders.Select(g => new GroupedOrders()
                                  {
                                      CreatedAt = g.Key.Created,
                                      DispatchedAt = g.Key.Dispatched,
                                      DeliveredAt = g.Key.Delivered,
                                      Status = g.Key.Status,
                                      Orders = g.Select(order => order),
                                      OrdersTotal = g.Sum(order => order.Total)
                                  })
                                  .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                                  .Take(paginationQuery.PageSize);
        }
    }
}
