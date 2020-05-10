using Pharmacy.Application.Common.Models;
using Pharmacy.Domain.Entites;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<TokenModel> RefreshToken(TokenModel userToken);

        Task Revoke(string userEmail);
    }
}
