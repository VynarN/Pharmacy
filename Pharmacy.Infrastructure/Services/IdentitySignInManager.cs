using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Entites;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Services
{
    public class IdentitySignInManager: ISignInManager
    {
        private readonly SignInManager<User> _manager;

        public IdentitySignInManager(SignInManager<User> manager)
        {
            _manager = manager;
        }

        public async Task<bool> SignInAsync(string email, string password)
        {
            return (await _manager.PasswordSignInAsync(email, password, false, false)).Succeeded;
        }
    }
}
