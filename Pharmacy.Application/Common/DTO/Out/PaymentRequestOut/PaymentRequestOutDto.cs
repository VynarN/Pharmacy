using Pharmacy.Domain.Entites;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.Out.PaymentRequestOut
{
    public class PaymentRequestOutDto: IMapFrom<PaymentRequest>
    {
        public int Id { get; set; }

        public string ReceiverEmail { get; set; }

        public int MedicamentId { get; set; }
        public MedicamentOutDto Medicament { get; set; }

        public int Quantity { get; set; }

        public int DeliveryAddressId { get; set; }
        public Address DeliveryAddress { get; set; }

        public decimal Total { get; set; }
    }
}
