using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class ApplicationMethodService : IApplicationMethodService
    {
        private readonly IRepository<ApplicationMethod> _repository;

        public ApplicationMethodService(IRepository<ApplicationMethod> repository)
        {
            _repository = repository;
        }

        public async Task CreateApplicationMethod(string applicationMethod)
        {

            await _repository.Create(new ApplicationMethod() { Method = applicationMethod });
        }

        public async Task DeleteApplicationMethod(ApplicationMethod applicationMethod)
        {
            await _repository.Delete(applicationMethod);
        }

        public IEnumerable<ApplicationMethod> GetApplicationMethods()
        {
            return _repository.GetAllQueryable();
        }
    }
}
