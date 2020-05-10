using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Services
{
    public class IdentityUserManager : IUserManager
    {
        private readonly UserManager<User> _manager;

        public IdentityUserManager(UserManager<User> userManager)
        {
            _manager = userManager;
        }

        public async Task AddUserToRoleAsync(User user, string role)
        {
            await _manager.AddToRoleAsync(user, role);
        }

        public async  Task<bool> ConfirmEmailAsync(User user, string token)
        {
            return (await _manager.ConfirmEmailAsync(user, token)).Succeeded;
        }

        public async Task CreateUserAsync(User user, string password)
        {
            await _manager.CreateAsync(user, password);
        }

        public async Task DeleteAsync(User user)
        {
            await _manager.DeleteAsync(user);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _manager.FindByEmailAsync(email);
        }

        public async Task<User> FindByIdAsync(string id)
        {
            return await _manager.FindByIdAsync(id);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _manager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _manager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IList<string>> GetUserRolesAsync(User user)
        {
            return await _manager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<User>> GetUsersInRoleAsync(string role)
        {
            return await _manager.GetUsersInRoleAsync(role);
        }

        public async Task<bool> ResetPasswordAsync(User user, string token, string newPassword)
        {
            return (await _manager.ResetPasswordAsync(user, token, newPassword)).Succeeded;
        }

        public async Task SetEmailAsync(User user, string email)
        {
            await _manager.SetEmailAsync(user, email);
        }

        public async Task SetPhoneNumberAsync(User user, string phoneNumber)
        {
            await _manager.SetPhoneNumberAsync(user, phoneNumber);
        }

        public async Task SetUserNameAsync(User user, string userName)
        {
            await _manager.SetUserNameAsync(user, userName);
        }

        public async Task UpdateAsync(User user)
        {
            await _manager.UpdateAsync(user);
        }
    }
}
