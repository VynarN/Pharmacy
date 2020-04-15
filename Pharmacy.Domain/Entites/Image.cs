namespace Pharmacy.Domain.Entites
{
    public class Image
    {
        public long Id { get; set; }

        public byte[] ImageData { get; set; }

        public long MedicamentId { get; set; }

        public Medicament Medicament { get; set; }
    }
}
