using Pharmacy.Domain.Entites;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.In.MedicamentIn
{
    public class MedicamentUpdateInfoDto: IMapFrom<Medicament>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public InstructionDto Instruction { get; set; }

        public AllowedForEntityDto AllowedForEntity { get; set; }
    }
}
