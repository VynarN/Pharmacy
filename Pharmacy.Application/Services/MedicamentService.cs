using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class MedicamentService : IMedicamentService
    {
        private readonly IRepository<Medicament> _repository;

        public MedicamentService(IRepository<Medicament> repository)
        {
            _repository = repository;   
        }

        public async Task<int> CreateMedicament(Medicament medicament)
        {
            await _repository.Create(medicament);

            var createdMedicamentId = _repository.GetByPredicate(
                    med => med.Name.Equals(medicament.Name) && med.Price == medicament.Price).FirstOrDefault()?.Id;

            return createdMedicamentId.HasValue ? createdMedicamentId.Value : throw new ObjectCreateException(ExceptionStrings.ObjectCreateException, medicament.Name);
        }

        public Task DeleteMedicament(int medicamentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Medicament> GetMedicament(int medicamentId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Medicament> GetMedicaments(PaginationQuery paginationQuery = null, MedicamentFilterQuery filterQuery = null)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateMedicament(Medicament medicament)
        {
            throw new System.NotImplementedException();
        }
    }
}
