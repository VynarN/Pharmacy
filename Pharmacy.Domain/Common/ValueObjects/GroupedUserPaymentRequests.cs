using System.Collections.Generic;

namespace Pharmacy.Domain.Common.ValueObjects
{
    public class GroupedUserPaymentRequests
    {
        public string SenderId { get; set; }

        public IEnumerable<GroupedPaymentRequests> GroupedPaymentRequests { get; set; }
    }
}
