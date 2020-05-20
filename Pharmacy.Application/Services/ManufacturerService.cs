using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Entites;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IRepository<Manufacturer> _repository;

        public ManufacturerService(IRepository<Manufacturer> repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateManufacturer(Manufacturer manufacturer)
        {
            await _repository.Create(manufacturer);

            return _repository.GetByPredicate(obj => obj.Name.Equals(manufacturer.Name)).FirstOrDefault().Id;
        }

        public async Task UpdateManufacturer(Manufacturer manufacturer)
        {
            await _repository.Update(manufacturer);
        }
    }
}
