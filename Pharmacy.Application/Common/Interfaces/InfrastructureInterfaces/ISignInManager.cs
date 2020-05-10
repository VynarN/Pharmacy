using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces
{
    public interface ISignInManager
    {
        Task<bool> SignInAsync(string email, string password);
    }
}
