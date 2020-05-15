using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Entites;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IMedicamentService
    {
        Task<Medicament> GetMedicament(int medicamentId);

        IQueryable<Medicament> GetMedicaments(out int totalMedicamentsCount, PaginationQuery paginationQuery, MedicamentFilterQuery filterQuery = null);

        Task<int> CreateMedicament(Medicament medicament);

        Task DeleteMedicament(int medicamentId);

        Task UpdateMedicament(Medicament medicament);
    }
}
