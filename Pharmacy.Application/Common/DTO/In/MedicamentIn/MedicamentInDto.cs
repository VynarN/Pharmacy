using Microsoft.AspNetCore.Http;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using static Pharmacy.Application.Common.Mappings.IMapFrom;

namespace Pharmacy.Application.Common.DTO.In.MedicamentIn
{
    public class MedicamentInDto: IMapFrom<Medicament>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

        public int Offtake { get; set; }

        public int AllowedForEntityId { get; set; }

        public int CategoryId { get; set; }

        public int MedicamentFormId { get; set; }

        public int ApplicationMethodId { get; set; }

        public int ManufacturerId { get; set; }

        public InstructionDto instruction { get; set; }

        public List<IFormFile> Images { get; set; }
    }
}
