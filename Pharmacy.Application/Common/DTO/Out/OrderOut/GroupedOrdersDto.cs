using Pharmacy.Domain.Common.ValueObjects;
using System.Collections.Generic;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.Out.OrderOut
{
    public class GroupedOrdersDto: IMapFrom<GroupedOrders>
    {
        public IEnumerable<OrderOutDto> Orders { get; set; }

        public decimal OrdersTotal { get; set; }

        public string Date { get; set; }
    }
}
