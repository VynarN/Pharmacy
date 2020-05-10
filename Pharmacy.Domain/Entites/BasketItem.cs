using System;

namespace Pharmacy.Domain.Entites
{
    public class BasketItem
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int MedicamentId { get; set; }
        public Medicament Medicament { get; set; }

        public int ProductQuantity { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BasketItem item &&
                   UserId == item.UserId &&
                   MedicamentId == item.MedicamentId &&
                   ProductQuantity == item.ProductQuantity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, MedicamentId, ProductQuantity);
        }
    }
}
