﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Pharmacy.Domain.Entites
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string RefreshToken { get; set; }

        public List<BasketItem> BasketItems { get; set; }

        public List<Order> Orders { get; set; }

        public User()
        {
            BasketItems = new List<BasketItem>();
            Orders = new List<Order>();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as User);
        }
        
        public bool Equals(User user)
        {
            return user != null && user.Email.Equals(Email) 
                   && user.PhoneNumber.Equals(PhoneNumber);
        }

        public override int GetHashCode()
        {
            var HashCode = 123579284;
            HashCode *= FirstName?.GetHashCode() ?? 1212223;
            HashCode *= SecondName?.GetHashCode() ?? 1323223;
            HashCode *= Email?.GetHashCode() ?? 1423345;
            HashCode *= PhoneNumber?.GetHashCode() ?? 1523534;
            HashCode *= Id?.GetHashCode() ?? 1234434;

            return HashCode;
        }
    }
}
