using System.Collections.Generic;

namespace Pharmacy.Domain.Entites
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Medicament> Medicaments { get; set; }

        public Category()
        {
            Medicaments = new List<Medicament>();
        }
    }
}
