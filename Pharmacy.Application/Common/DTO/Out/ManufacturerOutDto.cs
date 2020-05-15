using AutoMapper;
using Pharmacy.Domain.Entites;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.Out
{
    public class ManufacturerOutDto: IMapFrom<Manufacturer>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string WebSite { get; set; }

        public AddressDto Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Manufacturer, ManufacturerOutDto>()
                   .ForMember(m => m.Address, mf => mf.MapFrom(p => p.Address));
        }
    }
}
