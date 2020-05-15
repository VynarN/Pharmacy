using Pharmacy.Domain.Entites;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.Out
{
    public class ApplicationMethodOutDto: IMapFrom<ApplicationMethod>
    {
        public int Id { get; set; }

        public string Method { get; set; }
    }
}
