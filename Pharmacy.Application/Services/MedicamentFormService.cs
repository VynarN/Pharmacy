using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class MedicamentFormService : IMedicamentFormService
    {
        private readonly IRepository<MedicamentForm> _repository;

        public MedicamentFormService(IRepository<MedicamentForm> repository)
        {
            _repository = repository;
        }

        public async Task CreateApplicationMethod(string medicamentForm)
        {
            await _repository.Create(new MedicamentForm() { Form = medicamentForm });
        }

        public Task DeleteApplicationMethod(int medicamentFormId)
        {
            throw new System.NotImplementedException();
        }

        public Task<MedicamentForm> GetApplicationMethod(int medicamentFormId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<MedicamentForm>> GetApplicationMethods()
        {
            throw new System.NotImplementedException();
        }
    }
}
