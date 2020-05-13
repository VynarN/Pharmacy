using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Entites;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class AddressService: IAddressService
    {
        private readonly IRepository<Address> _repository;

        public AddressService(IRepository<Address> repository)
        {
            _repository = repository;
        }

        public async Task CreateAddress(Address address)
        {
            await _repository.Create(address);
        }
    }
}
