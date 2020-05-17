using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Common.Enums;
using Pharmacy.Domain.Entites;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repository;

        public OrderService(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task CreateOrder(Order order)
        {
            await _repository.Create(order);
        }

        public Task UpdateOrder(OrderStatus orderStatus, int orderId)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Order> GetOrders(PaginationQuery paginationQuery)
        {
            return _repository.GetWithInclude(obj => obj.DeliveryAddress, obj => obj.Medicament)
                              .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                              .Take(paginationQuery.PageSize);
        }
    }
}
