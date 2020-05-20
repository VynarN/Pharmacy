using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Validators;
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

        public async Task CreateMedicamentForm(string medicamentForm)
        {
            StringArgumentValidator.ValidateStringArgument(medicamentForm, nameof(medicamentForm));

            var correctedForm = char.ToUpper(medicamentForm[0]) + medicamentForm.Substring(1).ToLower();

            await _repository.Create(new MedicamentForm() { Form = correctedForm });
        }

        public async Task DeleteMedicamentForm(MedicamentForm medicamentForm)
        {
            await _repository.Delete(medicamentForm);
        }

        public IEnumerable<MedicamentForm> GetMedicamentForms()
        {
            return _repository.GetAllQueryable();
        }
    }
}
