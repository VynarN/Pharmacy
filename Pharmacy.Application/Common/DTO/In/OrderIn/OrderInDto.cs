using Pharmacy.Domain.Entites;

namespace Pharmacy.Application.Common.DTO.In.OrderIn
{
    public class OrderInDto
    {
        public string UserId { get; set; }

        public int MedicamentId { get; set; }

        public int Quantity { get; set; }

        public int DeliveryAddressId { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
    }
}
