using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Entites;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class AllowedForEntityService: IAllowedForEntityService
    {
        private readonly IRepository<AllowedForEntity> _repository;

        public AllowedForEntityService(IRepository<AllowedForEntity> repository)
        {
            _repository = repository;
        }

        public async Task CreateAllowedForEntity(AllowedForEntity allowedForEntity)
        {
            await _repository.Create(allowedForEntity);
        }
    }
}
