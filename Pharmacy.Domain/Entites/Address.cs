namespace Pharmacy.Domain.Entites
{
    public class Address
    {
        public int Id { get; set; }

        public int ZipCode { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int? ManufacturerId { get; set; }
        public Manufacturer Manufacturer{ get; set; }

        public int? OrderId { get; set; }
        public Order Order { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as Address);
        }

        public bool Equals(Address address)
        {
            return address != null && address.ZipCode == ZipCode
                   && address.Country.Equals(Country)
                   && address.City.Equals(City)
                   && address.Street.Equals(Street)
                   && address.Region.Equals(Region);
        }

        public override int GetHashCode()
        {
            var HashCode = 123449284;
            HashCode *= ZipCode * 121;
            HashCode *= City?.GetHashCode() ?? 132;
            HashCode *= Country?.GetHashCode() ?? 142;
            HashCode *= Region?.GetHashCode() ?? 152;
            HashCode *= Street?.GetHashCode() ?? 123;

            return HashCode;
        }
    }
}
