namespace Pharmacy.Domain.Entites
{
    public class BasketItem
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int MedicamentId { get; set; }
        public Medicament Medicament { get; set; }

        public int ProductQuantity { get; set; }
    }
}
