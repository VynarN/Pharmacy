using Pharmacy.Domain.Common.Enums;
using System;

namespace Pharmacy.Domain.Entites
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int MedicamentId { get; set; }
        public Medicament Medicament { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }
        
        public int DeliveryAddressId { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime OrderedAt { get; set; }

        public DateTime DispatchedAt { get; set; }

        public DateTime DeliveredAt { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   UserId == order.UserId &&
                   MedicamentId == order.MedicamentId &&
                   DeliveryAddressId == order.DeliveryAddressId &&
                   Quantity == order.Quantity &&
                   Total == order.Total &&
                   Status == order.Status &&
                   OrderedAt == order.OrderedAt &&
                   DispatchedAt == order.DispatchedAt &&
                   DeliveredAt == order.DeliveredAt;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, MedicamentId, DeliveryAddressId, Total, Status, OrderedAt, DispatchedAt, DeliveredAt) + Quantity.GetHashCode();
        }
    }
}
