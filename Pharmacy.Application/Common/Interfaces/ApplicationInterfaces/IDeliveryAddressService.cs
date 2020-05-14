using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IDeliveryAddressService
    {
        IEnumerable<DeliveryAddress> GetDeliveryAddresses(string userId);

        Task CreateDeliveryAddress(DeliveryAddress deliveryAddress);
    }
}
