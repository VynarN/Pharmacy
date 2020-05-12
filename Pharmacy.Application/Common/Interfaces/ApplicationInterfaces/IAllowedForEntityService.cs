using Pharmacy.Application.Common.DTO;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.ApplicationInterfaces
{
    public interface IAllowedForEntityService
    {
        Task CreateAllowedForEntityService(AllowedForEntityDto allowedForEntityDto);
    }
}
