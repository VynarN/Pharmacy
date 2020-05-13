using Pharmacy.Domain.Entites;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.In.ManufacturerIn
{
    public class ManufacturerInDto: IMapFrom<Manufacturer>
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string WebSite { get; set; }

        public AddressDto Address { get; set; }
    }
}
