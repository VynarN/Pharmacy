using System.Collections.Generic;

namespace Pharmacy.Domain.Entites
{
    public class Manufacturer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string WebSite { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

        public List<Medicament> Medicaments { get; set; }

        public Manufacturer()
        {
            Medicaments = new List<Medicament>();
        }
    }
}
