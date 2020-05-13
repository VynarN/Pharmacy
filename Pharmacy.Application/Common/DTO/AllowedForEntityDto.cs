using Pharmacy.Domain.Entites;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO
{
    public class AllowedForEntityDto: IMapFrom<AllowedForEntity>
    {
        public bool ForAdults { get; set; }

        public bool ForChildren { get; set; }

        public bool ForPregnants { get; set; }

        public bool ForNurses { get; set; }

        public bool ForDrivers { get; set; }

        public bool ForDiabetics { get; set; }

        public bool ForAllergist { get; set; }
    }
}
