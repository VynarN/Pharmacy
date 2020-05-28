using System.Collections.Generic;

namespace Pharmacy.Domain.Entites
{
    public class DeliveryAddress
    {
        public int Id { get; set; }

        public int ZipCode { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public List<Order> Orders { get; set; }

        public List<PaymentRequest> PaymentRequests { get; set; }

        public DeliveryAddress()
        {
            Orders = new List<Order>();
            PaymentRequests = new List<PaymentRequest>();
        }

        public override bool Equals(object obj)
        {
            var deliveryAddress = obj as DeliveryAddress;

            return deliveryAddress != null && deliveryAddress.ZipCode == ZipCode
                   && deliveryAddress.Country.Equals(Country)
                   && deliveryAddress.City.Equals(City)
                   && deliveryAddress.Street.Equals(Street)
                   && deliveryAddress.Region.Equals(Region);
        }

        public override int GetHashCode()
        {
            var HashCode = 123449284;
            HashCode *= ZipCode + HashCode;
            HashCode *= City?.GetHashCode() ?? 132;
            HashCode *= Country?.GetHashCode() ?? 142;
            HashCode *= Region?.GetHashCode() ?? 152;
            HashCode *= Street?.GetHashCode() ?? 123;

            return HashCode;
        }
    }
}
