using Pharmacy.Domain.Entites;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.HelpersInterfaces
{
    public interface IUserHelper
    {
        Task<User> FindUserByIdAsync(string id);

        Task<User> FindUserByEmailAsync(string email);
    }
}
