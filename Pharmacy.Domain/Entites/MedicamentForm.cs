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

        public override bool Equals(object obj)
        {
            return Equals(obj as MedicamentForm);
        }

        public bool Equals(MedicamentForm medicamentForm)
        {
            return medicamentForm != null && medicamentForm.Form.Equals(Form);
        }

        public override int GetHashCode()
        {
            return Form.GetHashCode() * 12;
        }
    }
}
