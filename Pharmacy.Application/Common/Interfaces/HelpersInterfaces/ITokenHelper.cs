using Pharmacy.Domain.Entites;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.HelpersInterfaces
{
    public interface ITokenHelper {

        Task<string> GenerateAccessToken(User user);

        string GenerateRefreshToken();

        Task<User> GetUserFromExpiredToken(string token);
    }
}

