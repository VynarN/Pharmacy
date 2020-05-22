using Pharmacy.Domain.Entites;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IBasketItemService
    {
        Task CreateBasketItem(BasketItem basketItem);

        Task DeleteBasketItem(BasketItem basketItem);

        IQueryable<BasketItem> GetBasketItems(string userId);

        Task UpdateBasketItem(BasketItem basketItem);
    }
}
