using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Common.Enums;
using Pharmacy.Domain.Common.ValueObjects;
using Pharmacy.Domain.Entites;
using System;
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

        public async Task CreateOrder(string userId, DeliveryAddress deliveryAddress)
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
                OrderedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending
            });

            await _orderRepository.Create(orders);

            await _basketItemsRepo.Delete(userBasketItems);
        }

        public Task UpdateOrder(OrderStatus orderStatus, int orderId)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<GroupedOrders> GetOrders(PaginationQuery paginationQuery)
        {
            return _orderRepository.GetWithInclude(obj => obj.DeliveryAddress, obj => obj.Medicament)
                                   .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                                   .Take(paginationQuery.PageSize)
                                   .AsEnumerable()
                                   .GroupBy(order => order.OrderedAt.ToString("dd/mm/yyyy H:mm"))
                                   .Select(g => new GroupedOrders() { 
                                       Date = g.Key, 
                                       Orders = g.Select(order => order), 
                                       OrdersTotal = g.Sum(order => order.Total) })
                                   .AsQueryable();
        }

        public IQueryable<GroupedOrders> GetUserOrders(PaginationQuery paginationQuery, string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
