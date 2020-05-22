using AutoMapper;
using Pharmacy.Domain.Entites;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.Out
{
    public class BasketItemOutDto: IMapFrom<BasketItem>
    {
        public MedicamentBaseInfoDto Medicament { get; set; }

        public int ProductQuantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BasketItem, BasketItemOutDto>()
                   .ForMember(dto => dto.Medicament, c => c.MapFrom(ent => ent.Medicament));
        }
    }
}
