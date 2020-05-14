using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Common.Interfaces.ApplicationInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> CreateAllowedForEntity(AllowedForEntity allowedForEntity)
        {
            var existingEntityId = ExistingEntities(allowedForEntity).FirstOrDefault()?.Id;

            if (existingEntityId.HasValue)
            {
                return existingEntityId.Value;
            }
            else
            {
                await _repository.Create(allowedForEntity);

                var createdEntityId = ExistingEntities(allowedForEntity).FirstOrDefault()?.Id;

                return createdEntityId.HasValue ? createdEntityId.Value :
                    throw new ObjectCreateException(ExceptionStrings.ObjectCreateException, nameof(allowedForEntity));
            }

            IEnumerable<AllowedForEntity> ExistingEntities(AllowedForEntity allowedFor)
            {
                return _repository.GetByPredicate(obj => obj.ForAdults == allowedFor.ForAdults
                          && obj.ForChildren == allowedFor.ForChildren
                          && obj.ForAllergist == allowedFor.ForAllergist
                          && obj.ForDiabetics == allowedFor.ForDiabetics
                          && obj.ForDrivers == allowedFor.ForDrivers
                          && obj.ForNurses == allowedFor.ForNurses
                          && obj.ForPregnants == allowedFor.ForPregnants);
            }
        }
    }
}
