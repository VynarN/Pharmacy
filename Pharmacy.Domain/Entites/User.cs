using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Pharmacy.Domain.Entites
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string RefreshToken { get; set; }

        public DateTime LastActive { get; set; } = DateTime.MinValue;

        public List<Order> Orders { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }
    }
}
