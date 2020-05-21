using Pharmacy.Domain.Common.ValueObjects;
using System.Collections.Generic;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.Out.PaymentRequestOut
{
    public class GroupedInPaymentRequestsDto: IMapFrom<GroupedIncomingPaymentRequest>
    {
        public IEnumerable<PaymentRequestDto> PaymentRequests { get; set; }

        public string SenderEmail { get; set; }

        public string Status { get; set; }

        public string Created { get; set; }
    }
}
