using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Entites;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class BasketItemService : IBasketItemService
    {
        private readonly IRepository<BasketItem> _repository;

        public BasketItemService(IRepository<BasketItem> repository)
        {
            _repository = repository;
        }

        public async Task CreateBasketItem(BasketItem basketItem)
        {
            await _repository.Create(basketItem);
        }

        public Task DeleteBasketItem(int basketItemId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BasketItem> GetBasketItems()
        {
            throw new NotImplementedException();
        }

        public Task UpdateBasketItem(int medicamentCount, int basketItemId)
        {
            throw new NotImplementedException();
        }
    }
}
