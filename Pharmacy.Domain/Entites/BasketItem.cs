namespace Pharmacy.Domain.Entites
{
    public class BasketItem
    {
        public string UserId { get; set; }

        public long MedicamentId { get; set; }
        public Medicament Medicament { get; set; }

        public int ProductQuantity { get; set; }
    }
}
