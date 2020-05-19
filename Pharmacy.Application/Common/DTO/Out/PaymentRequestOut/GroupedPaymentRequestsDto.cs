using Pharmacy.Domain.Common.ValueObjects;
using System.Collections.Generic;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.Out.PaymentRequestOut
{
    public class GroupedPaymentRequestsDto: IMapFrom<GroupedPaymentRequests>
    {
        public IEnumerable<PaymentRequestOutDto> PaymentRequests { get; set; }

        public string Status { get; set; }

        public string Created { get; set; }
    }
}
