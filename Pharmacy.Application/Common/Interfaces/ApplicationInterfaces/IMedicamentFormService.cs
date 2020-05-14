using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IMedicamentFormService
    {
        IEnumerable<MedicamentForm> GetMedicamentForms();

        Task CreateMedicamentForm(string medicamentForm);

        Task DeleteMedicamentForm(int medicamentFormId);
    }
}
