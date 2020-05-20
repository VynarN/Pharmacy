using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IApplicationMethodService
    {
        IEnumerable<ApplicationMethod> GetApplicationMethods();

        Task CreateApplicationMethod(string applicationMethod);

        Task DeleteApplicationMethod(ApplicationMethod applicationMethod);
    }
}
