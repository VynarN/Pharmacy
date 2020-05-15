using Pharmacy.Domain.Entites;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.Out
{
    public class MedicamentFormOutDto: IMapFrom<MedicamentForm>
    {
        public int Id { get; set; }

        public string Form { get; set; }
    }
}
