using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.HelpersInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Entites;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class MedicamentService : IMedicamentService
    {
        private readonly IRepository<Medicament> _repository;
        private readonly IFilterHelper<Medicament, MedicamentFilterQuery> _filterHelper;

        public MedicamentService(IRepository<Medicament> repository, IFilterHelper<Medicament, MedicamentFilterQuery> filterHelper)
        {
            _repository = repository;
            _filterHelper = filterHelper;
        }

        public async Task<int> CreateMedicament(Medicament medicament)
        {
            await _repository.Create(medicament);

            return  _repository.GetByPredicate( med => med.Name.Equals(medicament.Name) && med.Price == medicament.Price).FirstOrDefault().Id;
        }

        public async Task DeleteMedicament(Medicament medicament)
        {
            await _repository.Delete(medicament);
        }
        
        public Medicament GetMedicament(int medicamentId)
        {
            return _repository.GetWithInclude(obj => obj.Id == medicamentId, obj => obj.Images,
                                                                             obj => obj.Instruction,
                                                                             obj => obj.Manufacturer,
                                                                             obj => obj.MedicamentForm,
                                                                             obj => obj.ApplicationMethod,
                                                                             obj => obj.Category,
                                                                             obj => obj.AllowedForEntity)
                              .FirstOrDefault() ?? throw new ObjectNotFoundException(ExceptionStrings.ObjectNotFoundException, medicamentId.ToString());
        }

        public IQueryable<Medicament> GetMedicaments(out int totalMedicamentsCount, PaginationQuery paginationQuery, MedicamentFilterQuery filterQuery)
        {
            totalMedicamentsCount = _repository.GetAllQueryable().Count();

            var medicamentsWithIncludes = _repository.GetWithInclude(prop => prop.AllowedForEntity);

            return _filterHelper.Filter(medicamentsWithIncludes, filterQuery)
                                .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                                .Take(paginationQuery.PageSize);
        }

        public async Task UpdateMedicament(Medicament medicament)
        {
            await _repository.Update(medicament);
        }
    }
}
