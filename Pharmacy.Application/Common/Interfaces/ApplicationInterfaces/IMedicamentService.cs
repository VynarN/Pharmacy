using Pharmacy.Application.Common.DTO.In.MedicamentIn;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IMedicamentService
    {
        Task<Medicament> GetMedicament(int medicamentId);

        Task<IEnumerable<Medicament>> GetMedicaments();

        Task CreateMedicament(MedicamentInDto medicamentDto);

        Task DeleteMedicament(int medicamentId);

        Task UpdateMedicament(MedicamentInDto medicamentDto);
    }
}
