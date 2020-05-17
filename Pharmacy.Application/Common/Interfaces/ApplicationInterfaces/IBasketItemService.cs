using Pharmacy.Domain.Entites;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IBasketItemService
    {
        Task CreateBasketItem(BasketItem basketItem);

        Task DeleteBasketItem(int basketItemId);

        IQueryable<BasketItem> GetBasketItems();

        Task UpdateBasketItem(int medicamentCount, int basketItemId);
    }
}
