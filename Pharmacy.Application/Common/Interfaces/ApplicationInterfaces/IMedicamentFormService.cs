using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IMedicamentFormService
    {
        Task<MedicamentForm> GetApplicationMethod(int medicamentFormId);

        Task<IEnumerable<MedicamentForm>> GetApplicationMethods();

        Task CreateApplicationMethod(string medicamentForm);

        Task DeleteApplicationMethod(int medicamentFormId);
    }
}
