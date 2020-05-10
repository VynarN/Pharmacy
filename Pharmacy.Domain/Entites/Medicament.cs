using Pharmacy.Domain.Common.ValueObjects;
using System;
using System.Collections.Generic;

namespace Pharmacy.Domain.Entites
{
    public class Medicament : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public int Offtake { get; set; }

        public Instruction Instruction { get; set; }

        public AllowedForEntity AllowedForEntity { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int MedicamentFormId { get; set; }
        public MedicamentForm MedicamentForm { get; set; }

        public int ApplicationMethodId { get; set; }
        public ApplicationMethod ApplicationMethod { get; set; }

        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public List<Image> Images { get; set; }

        public List<Order> Orders { get; set; }

        public Medicament()
        {
            Images = new List<Image>();
            Orders = new List<Order>();
        }

        public override bool Equals(object obj)
        {
            return obj is Medicament medicament &&
                   Name == medicament.Name &&
                   Price == medicament.Price &&
                   QuantityInStock == medicament.QuantityInStock &&
                   Offtake == medicament.Offtake &&
                   Instruction.Equals(medicament.Instruction) &&
                   AllowedForEntity.Equals(medicament.AllowedForEntity) &&
                   CategoryId == medicament.CategoryId &&
                   MedicamentFormId == medicament.MedicamentFormId &&
                   ApplicationMethodId == medicament.ApplicationMethodId &&
                   ManufacturerId == medicament.ManufacturerId;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Name);
            hash.Add(Price);
            hash.Add(QuantityInStock);
            hash.Add(Offtake);
            hash.Add(Instruction);
            hash.Add(AllowedForEntity);
            hash.Add(CategoryId);
            hash.Add(MedicamentFormId);
            hash.Add(ApplicationMethodId);
            hash.Add(ManufacturerId);
            return hash.ToHashCode();
        }
    }
}
