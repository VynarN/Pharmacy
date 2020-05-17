using AutoMapper;
using Pharmacy.Domain.Common.Enums;
using Pharmacy.Domain.Entites;
using System;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.Out
{
    public class OrderOutDto : IMapFrom<Order>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public MedicamentOutDto Medicament { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }

        public DeliveryAddressDto DeliveryAddress { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime OrderedAt { get; set; }

        public DateTime DispatchedAt { get; set; }

        public DateTime DeliveredAt { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderOutDto>()
                .ForMember(o => o.DeliveryAddress, mf => mf.MapFrom(o => o.DeliveryAddress))
                .ForMember(o => o.Medicament, mf => mf.MapFrom(o => o.Medicament));
        }
    }
}
