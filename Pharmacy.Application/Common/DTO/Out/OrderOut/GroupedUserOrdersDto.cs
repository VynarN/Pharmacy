using Pharmacy.Domain.Common.ValueObjects;
using System.Collections.Generic;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.Out.OrderOut
{
    public class GroupedUserOrdersDto : IMapFrom<GroupedUserOrders>
    {
        public string UserId { get; set; }

        public IEnumerable<GroupedOrdersDto> GroupedOrders { get; set; }
    }
}
