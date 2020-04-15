using System.Collections.Generic;

namespace Pharmacy.Domain.Entites
{
    public class AllowedForEntity
    {
        public long Id { get; set; }

        public bool ForAdults { get; set; }

        public bool ForChildren { get; set; }

        public bool ForPregnants { get; set; }

        public bool ForNurses { get; set; }

        public bool ForDrivers { get; set; }

        public bool ForDiabetics { get; set; }

        public bool ForAllergist { get; set; }

        public List<Medicament> Medicaments { get; set; }

        public AllowedForEntity()
        {
            Medicaments = new List<Medicament>();
        }
    }
}
