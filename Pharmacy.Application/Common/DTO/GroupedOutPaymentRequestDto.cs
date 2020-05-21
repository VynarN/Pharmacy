using Pharmacy.Domain.Common.ValueObjects;
using System.Collections.Generic;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO
{
    public class GroupedOutPaymentRequestDto: IMapFrom<GroupedOutcomingPaymentRequest>
    {
        public IEnumerable<PaymentRequestDto> PaymentRequests { get; set; }

        public string ReceiverEmail { get; set; }

        public string Status { get; set; }

        public string Created { get; set; }
    }
}
