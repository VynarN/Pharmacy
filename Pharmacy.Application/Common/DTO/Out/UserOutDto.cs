using System.Collections.Generic;

namespace Pharmacy.Application.Common.DTO.Out
{
    public class UserOutDto
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public List<OrderOutDto> Orders { get; set; }

        public List<BasketItemOutDto> BasketItems { get; set; }

        public UserOutDto()
        {
            Orders = new List<OrderOutDto>();
            BasketItems = new List<BasketItemOutDto>();
        }
    }
}
