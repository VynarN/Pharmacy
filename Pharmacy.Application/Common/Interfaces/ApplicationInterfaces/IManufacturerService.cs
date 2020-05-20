using Pharmacy.Domain.Entites;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IManufacturerService
    {
        Task<int> CreateManufacturer(Manufacturer manufacturer);

        Task UpdateManufacturer(Manufacturer manufacturer);
    }
}
