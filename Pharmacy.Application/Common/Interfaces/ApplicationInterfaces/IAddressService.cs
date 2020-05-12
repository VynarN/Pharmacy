using Pharmacy.Application.Common.DTO;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IAddressService
    {
        Task CreateAddress(AddressDto addressDto);
    }
}
