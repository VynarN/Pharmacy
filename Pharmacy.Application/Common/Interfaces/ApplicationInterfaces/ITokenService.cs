using Pharmacy.Application.Common.AppObjects;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<Tokens> RefreshToken(string accessToken, string refreshToken);

        Task Revoke(string userEmail);
    }
}
