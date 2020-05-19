using System.Collections.Generic;

namespace Pharmacy.Domain.Common.ValueObjects
{
    public class GroupedUserOrders
    {
        public string UserId { get; set; }

        public IEnumerable<GroupedOrders> GroupedOrders { get; set; }
    }
}
