using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IDeliveryAddressService
    {
        Task<IEnumerable<DeliveryAddress>> GetDeliveryAddresses();

        Task CreateDeliveryAddress(DeliveryAddress deliveryAddress);
    }
}
