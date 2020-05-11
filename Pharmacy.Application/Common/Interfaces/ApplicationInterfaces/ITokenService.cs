using Pharmacy.Application.Common.AppObjects;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<Tokens> RefreshToken(Tokens userToken);

        Task Revoke(string userEmail);
    }
}
