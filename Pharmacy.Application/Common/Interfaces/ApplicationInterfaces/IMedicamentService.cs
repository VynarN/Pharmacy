using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IMedicamentService
    {
        Task<Medicament> GetMedicament(int medicamentId);

        IEnumerable<Medicament> GetMedicaments(PaginationQuery paginationQuery = null, MedicamentFilterQuery filterQuery = null);

        Task<int> CreateMedicament(Medicament medicament);

        Task DeleteMedicament(int medicamentId);

        Task UpdateMedicament(Medicament medicament);
    }
}
