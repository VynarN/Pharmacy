﻿using Pharmacy.Domain.Common.ValueObjects;
using System.Collections.Generic;

namespace Pharmacy.Domain.Entites
{
    public class Medicament: AuditableEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Instruction { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public int Offtake { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int ApplicationMethodId { get; set; }
        public ApplicationMethod ApplicationMethod { get; set; }

        public int AllowedForEntityId { get; set; }
        public AllowedForEntity AllowedForEntity { get; set; }

        public int MedicamentFormId { get; set; }
        public MedicamentForm MedicamentForm { get; set; }

        public List<Image> Images { get; set; }

        public List<Order> Orders { get; set; }

        public Medicament()
        {
            Images = new List<Image>();
            Orders = new List<Order>();
        }

    }
}