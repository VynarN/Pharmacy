using Pharmacy.Domain.Entites;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.Out
{
    public class ImageOutDto: IMapFrom<Image>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }
    }
}
