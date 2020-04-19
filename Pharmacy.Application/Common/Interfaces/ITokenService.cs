using Pharmacy.Application.Common.Models;
using Pharmacy.Domain.Entites;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(User user);

        string GenerateRefreshToken();

        Task<User> GetUserFromExpiredToken(string token);

        Task<TokenModel> RefreshToken(TokenModel userToken);

        Task Revoke(string userEmail);
    }
}
