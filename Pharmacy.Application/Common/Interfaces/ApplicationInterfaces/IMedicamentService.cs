using Pharmacy.Application.Common.Queries;
using Pharmacy.Domain.Entites;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IMedicamentService
    {
        Medicament GetMedicament(int medicamentId);

        IQueryable<Medicament> GetMedicaments(out int totalMedicamentsCount, PaginationQuery paginationQuery, MedicamentFilterQuery filterQuery);

        Task<int> CreateMedicament(Medicament medicament);

        Task DeleteMedicament(Medicament medicament);

        Task UpdateMedicament(Medicament medicament);
    }
}
