using Pharmacy.Domain.Entites;
using System.Collections.Generic;

namespace Pharmacy.Domain.Common.ValueObjects
{
    public class GroupedOrders
    {
        public IEnumerable<Order> Orders { get; set; }

        public decimal OrdersTotal { get; set; }

        public string Date { get; set; }
    }
}
