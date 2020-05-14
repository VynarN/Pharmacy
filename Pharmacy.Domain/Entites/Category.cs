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

        public override bool Equals(object obj)
        {
            var category = obj as Category;

            return category != null && category.Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() * 125494351;
        }
    }
}
