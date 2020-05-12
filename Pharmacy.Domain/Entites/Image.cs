using System;

namespace Pharmacy.Domain.Entites
{
    public class Image
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public int MedicamentId { get; set; }

        public Medicament Medicament { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Image image &&
                   Name == image.Name &&
                   Uri == image.Uri &&
                   MedicamentId == image.MedicamentId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Uri, MedicamentId);
        }
    }
}
