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

        public async Task DeleteBasketItem(BasketItem basketItem)
        {
            await _repository.Delete(basketItem);
        }

        public IQueryable<BasketItem> GetBasketItems(string userId)
        {
            return _repository.GetWithInclude(bi => bi.UserId.Equals(userId), obj => obj.Medicament, obj =>obj.Medicament.Images );
        }

        public async Task UpdateBasketItem(BasketItem basketItem)
        {
            await _repository.Update(basketItem);
        }
    }
}
