using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class DeliveryAddressService : IDeliveryAddressService
    {
        private readonly IRepository<DeliveryAddress> _repository;

        public DeliveryAddressService(IRepository<DeliveryAddress> repository)
        {
            _repository = repository;
        }

        public async Task CreateDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            await _repository.Create(deliveryAddress);
        }

        public Task<IEnumerable<DeliveryAddress>> GetDeliveryAddresses()
        {
            throw new System.NotImplementedException();
        }
    }
}
