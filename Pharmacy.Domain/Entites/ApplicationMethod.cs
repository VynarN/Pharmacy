using System.Collections.Generic;

namespace Pharmacy.Domain.Entites
{
    public class ApplicationMethod
    {
        public int Id { get; set; }

        public string Method { get; set; }

        public List<Medicament> Medicaments { get; set; }

        public ApplicationMethod()
        {
            Medicaments = new List<Medicament>();
        }
    }
}
