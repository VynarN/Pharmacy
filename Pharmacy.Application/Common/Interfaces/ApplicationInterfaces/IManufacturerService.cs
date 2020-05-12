using Pharmacy.Application.Common.DTO.In.ManufacturerIn;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IManufacturerService
    {
        Task CreateManufacturer(ManufacturerInDto manufacturerDto);
    }
}
