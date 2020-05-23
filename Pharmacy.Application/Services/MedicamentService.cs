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
        private readonly IAllowedForEntityService _allowedForEntityService;
        private readonly IFilterHelper<Medicament, MedicamentFilterQuery> _filterHelper;

        public MedicamentService(IRepository<Medicament> repository, IFilterHelper<Medicament, MedicamentFilterQuery> filterHelper,
                                 IAllowedForEntityService allowedForEntityService)
        {
            _repository = repository;
            _filterHelper = filterHelper;
            _allowedForEntityService = allowedForEntityService;
        }

        public async Task<int> CreateMedicament(Medicament medicament)
        {
            var createdAllowedForEntityId = await _allowedForEntityService.CreateAllowedForEntity(medicament.AllowedForEntity);

            medicament.AllowedForEntityId = createdAllowedForEntityId;
            medicament.AllowedForEntity = null;

            await _repository.Create(medicament);

            return  _repository.GetByPredicate( med => med.Name.Equals(medicament.Name) && med.Price == medicament.Price).FirstOrDefault().Id;
        }

        public async Task DeleteMedicament(int medicamentId)
        {
            var medicamentToBeDeleted = (await _repository.GetByIdAsync(medicamentId)) 
                                      ?? throw new ObjectNotFoundException(ExceptionStrings.ObjectNotFoundException, medicamentId.ToString());

            await _repository.Delete(medicamentToBeDeleted);
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

            var medicamentsWithIncludes = _repository.GetWithInclude(prop => prop.AllowedForEntity, prop => prop.Manufacturer);

            return _filterHelper.Filter(medicamentsWithIncludes, filterQuery)
                                .Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize)
                                .Take(paginationQuery.PageSize);
        }

        public async Task UpdateMedicament(int medicamentId, Medicament medicament)
        {
            var medicamentToBeUpdated = _repository.GetWithInclude(med => med.Id == medicamentId, obj => obj.Instruction,
                                                                                                  obj => obj.AllowedForEntity).FirstOrDefault()
                                     ?? throw new ObjectNotFoundException(ExceptionStrings.ObjectNotFoundException, medicamentId.ToString());

            medicament.Name = medicament.Name.Equals(medicamentToBeUpdated.Name)
                                             ? medicamentToBeUpdated.Name
                                             : medicament.Name;

            medicamentToBeUpdated.Price = medicament.Price != medicamentToBeUpdated.Price 
                                                           ? medicament.Price 
                                                           : medicamentToBeUpdated.Price;

            medicamentToBeUpdated.QuantityInStock = medicament.QuantityInStock != medicamentToBeUpdated.QuantityInStock 
                                                                               ? medicament.QuantityInStock 
                                                                               : medicamentToBeUpdated.QuantityInStock;

            medicament.Instruction.MedicamentId = medicamentId;
            medicament.Instruction.Id = medicamentToBeUpdated.Id;
            medicamentToBeUpdated.Instruction = medicament.Instruction.Equals(medicamentToBeUpdated.Instruction) 
                                                                      ? medicamentToBeUpdated.Instruction 
                                                                      : medicament.Instruction;

            if (!medicament.AllowedForEntity.Equals(medicamentToBeUpdated.AllowedForEntity))
            {
                var newAllowedForEntityId = await _allowedForEntityService.CreateAllowedForEntity(medicament.AllowedForEntity);
                medicamentToBeUpdated.AllowedForEntity = null;
                medicamentToBeUpdated.AllowedForEntityId = newAllowedForEntityId;
            }

            await _repository.Update(medicamentToBeUpdated);
        }
    }
}
