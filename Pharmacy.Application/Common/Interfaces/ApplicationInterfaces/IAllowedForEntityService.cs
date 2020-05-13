using Pharmacy.Domain.Entites;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IAllowedForEntityService
    {
        Task CreateAllowedForEntity(AllowedForEntity allowedForEntity);
    }
}
