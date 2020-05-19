using Pharmacy.Domain.Entites;
using System.Collections.Generic;

namespace Pharmacy.Domain.Common.ValueObjects
{
    public class GroupedOrders
    {
        public IEnumerable<Order> Orders { get; set; }

        public decimal OrdersTotal { get; set; }

        public string CreatedAt { get; set; }

        public string DispatchedAt { get; set; }

        public string DeliveredAt { get; set; }

        public string Status { get; set; }
    }
}
