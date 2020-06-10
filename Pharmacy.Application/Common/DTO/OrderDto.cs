using AutoMapper;
using Pharmacy.Domain.Entites;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO
{
    public class OrderDto : IMapFrom<Order>
    {
        public int Id { get; set; }

        public MedicamentBaseInfoDto Medicament { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }

        public DeliveryAddressDto DeliveryAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDto>()
                .ForMember(o => o.DeliveryAddress, mf => mf.MapFrom(o => o.DeliveryAddress))
                .ForMember(o => o.Medicament, mf => mf.MapFrom(o => o.Medicament));
        }
    }
}
