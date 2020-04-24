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

        public override bool Equals(object obj)
        {
            return Equals(obj as ApplicationMethod);
        }

        public bool Equals(ApplicationMethod method)
        { 
            return method != null && method.Method.Equals(Method);
        }

        public override int GetHashCode()
        {
            return Method.GetHashCode() * 99;
        }
    }
}
