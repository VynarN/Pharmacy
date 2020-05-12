using System;
using System.Collections.Generic;

namespace Pharmacy.Domain.Entites
{
    public class AllowedForEntity
    {
        public int Id { get; set; }

        public bool ForAdults { get; set; }

        public bool ForChildren { get; set; }

        public bool ForPregnants { get; set; }

        public bool ForNurses { get; set; }

        public bool ForDrivers { get; set; }

        public bool ForDiabetics { get; set; }

        public bool ForAllergist { get; set; }

        public List<Medicament> Medicaments { get; set; }

        public override bool Equals(object obj)
        {
            return obj is AllowedForEntity entity &&
                   ForAdults == entity.ForAdults &&
                   ForChildren == entity.ForChildren &&
                   ForPregnants == entity.ForPregnants &&
                   ForNurses == entity.ForNurses &&
                   ForDrivers == entity.ForDrivers &&
                   ForDiabetics == entity.ForDiabetics &&
                   ForAllergist == entity.ForAllergist;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ForAdults, ForChildren, ForPregnants, ForNurses, ForDrivers, ForDiabetics, ForAllergist);
        }
    }
}
