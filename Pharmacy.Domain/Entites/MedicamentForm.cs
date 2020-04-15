using System.Collections.Generic;

namespace Pharmacy.Domain.Entites
{
    public class MedicamentForm
    {
        public int Id { get; set; }

        public string Form { get; set; }

        public List<Medicament> Medicaments { get; set; }

        public MedicamentForm()
        {
            Medicaments = new List<Medicament>();
        }
    }
}
