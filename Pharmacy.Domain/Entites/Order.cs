using Pharmacy.Domain.Common.Enums;
using System;

namespace Pharmacy.Domain.Entites
{
    public class Order
    {
        public long Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public long MedicamentId { get; set; }
        public Medicament Medicament { get; set; }

        public int ProductQuantity { get; set; }

        public decimal Total { get; set; }

        public string Address { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime OrderedAt { get; set; } = DateTime.Now;

        public DateTime DispatchedAt { get; set; } = DateTime.MinValue;

        public DateTime DeliveredAt { get; set; } = DateTime.MinValue;
    }
}
