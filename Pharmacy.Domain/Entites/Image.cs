namespace Pharmacy.Domain.Entites
{
    public class Image
    {
        public int Id { get; set; }

        public byte[] ImageData { get; set; }

        public int MedicamentId { get; set; }

        public Medicament Medicament { get; set; }
    }
}
