using AutoMapper;
using Pharmacy.Domain.Entites;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.Out
{
    public class MedicamentOutDto: IMapFrom<Medicament>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public int Offtake { get; set; }

        public CategoryOutDto Category { get; set; }

        public MedicamentFormOutDto MedicamentForm { get; set; }

        public ApplicationMethodOutDto ApplicationMethod { get; set; }

        public ManufacturerOutDto Manufacturer { get; set; }

        public InstructionDto Instruction { get; set; }

        public AllowedForEntityDto AllowedForEntity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Medicament, MedicamentOutDto>()
                .ForMember(m => m.Category, mf => mf.MapFrom(p => p.Category))
                .ForMember(m => m.AllowedForEntity, mf => mf.MapFrom(p => p.AllowedForEntity))
                .ForMember(m => m.ApplicationMethod, mf => mf.MapFrom(p => p.ApplicationMethod))
                .ForMember(m => m.Manufacturer, mf => mf.MapFrom(p => p.Manufacturer))
                .ForMember(m => m.Instruction, mf => mf.MapFrom(p => p.Instruction))
                .ForMember(m => m.MedicamentForm, mf => mf.MapFrom(p => p.MedicamentForm));
        }
    }
}
