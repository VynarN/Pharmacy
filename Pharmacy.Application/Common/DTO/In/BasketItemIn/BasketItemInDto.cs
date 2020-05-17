using Pharmacy.Domain.Entites;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.In.BasketItemIn
{
    public class BasketItemInDto: IMapFrom<BasketItem>
    {
        public int MedicamentId { get; set; }

        public int ProductQuantity { get; set; }
    }
}
