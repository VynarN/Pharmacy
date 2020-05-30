using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IManufacturerService
    {
        Task<int> CreateManufacturer(Manufacturer manufacturer);

        Task UpdateManufacturer(Manufacturer manufacturer);

        IEnumerable<Manufacturer> GetManufacturers();
    }
}
