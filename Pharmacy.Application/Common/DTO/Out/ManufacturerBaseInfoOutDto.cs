using Pharmacy.Domain.Entites;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.Out
{
    public class ManufacturerBaseInfoOutDto : IMapFrom<Manufacturer>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string WebSite { get; set; }
    }


}
