using AutoMapper;
using Pharmacy.Domain.Entites;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO
{
    public class PaymentRequestDto: IMapFrom<PaymentRequest>
    {
        public int Id { get; set; }

        public MedicamentBaseInfoDto Medicament { get; set; }

        public int Quantity { get; set; }

        public DeliveryAddressDto DeliveryAddress { get; set; }

        public decimal Total { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PaymentRequest, PaymentRequestDto>()
                    .ForMember(dto => dto.DeliveryAddress, mf => mf.MapFrom(obj => obj.DeliveryAddress))
                    .ForMember(dto => dto.Medicament, mf => mf.MapFrom(obj => obj.Medicament)).ReverseMap();
        }
    }
}
