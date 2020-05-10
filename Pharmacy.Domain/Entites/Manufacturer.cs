using System;
using System.Collections.Generic;

namespace Pharmacy.Domain.Entites
{
    public class Manufacturer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string WebSite { get; set; }

        public Address Address { get; set; }

        public List<Medicament> Medicaments { get; set; }

        public Manufacturer()
        {
            Medicaments = new List<Medicament>();
        }

        public override bool Equals(object obj)
        {
            return obj is Manufacturer manufacturer &&
                   Id == manufacturer.Id &&
                   Name == manufacturer.Name &&
                   PhoneNumber == manufacturer.PhoneNumber &&
                   WebSite == manufacturer.WebSite &&
                   Address.Equals(manufacturer.Address);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, PhoneNumber, WebSite, Address);
        }
    }
}
