using Pharmacy.Domain.Common.ValueObjects;
using System.Collections.Generic;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.Out.PaymentRequestOut
{
    public class GroupedUserPaymentRequestsDto: IMapFrom<GroupedUserPaymentRequests>
    {
        public string UserId { get; set; }

        public IEnumerable<GroupedPaymentRequestsDto> groupedPaymentRequests { get; set; }
    }
}
