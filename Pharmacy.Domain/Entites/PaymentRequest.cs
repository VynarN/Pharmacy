using Pharmacy.Domain.Common.Enums;
using System;

namespace Pharmacy.Domain.Entites
{
    public class PaymentRequest
    {
        public int Id { get; set; }

        public string SenderId { get; set; }
        public User Sender { get; set; }

        public string ReceiverEmail { get; set; }

        public int MedicamentId { get; set; }
        public Medicament Medicament { get; set; }

        public int Quantity { get; set; }

        public int DeliveryAddressId { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }

        public decimal Total { get; set; }

        public DateTime RequestedAt { get; set; }

        public RequestStatus RequestStatus { get; set; }

        public override bool Equals(object obj)
        {
            return obj is PaymentRequest request &&
                   SenderId == request.SenderId &&
                   ReceiverEmail == request.ReceiverEmail &&
                   MedicamentId == request.MedicamentId &&
                   DeliveryAddressId == request.DeliveryAddressId &&
                   Quantity == request.Quantity &&
                   Total == request.Total &&
                   RequestedAt == request.RequestedAt;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SenderId, ReceiverEmail, Quantity, MedicamentId, DeliveryAddressId, Total, RequestedAt);
        }
    }
}
