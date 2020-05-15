using Pharmacy.Domain.Entites;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.Out
{
    public class CategoryOutDto: IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
