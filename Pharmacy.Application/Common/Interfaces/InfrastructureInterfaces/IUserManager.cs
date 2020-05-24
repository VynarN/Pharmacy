using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces
{
    public interface IUserManager
    {
        Task CreateUserAsync(User user, string password);

        Task<User> FindByEmailAsync(string email);

        Task<User> FindByIdAsync(string id);

        Task<IList<string>> GetUserRolesAsync(User user);

        Task UpdateAsync(User user);

        Task DeleteAsync(User user);

        Task<string> GenerateEmailConfirmationTokenAsync(User user);

        Task<bool> ConfirmEmailAsync(User user, string token);

        Task<string> GeneratePasswordResetTokenAsync(User user);

        Task<bool> ResetPasswordAsync(User user, string token, string newPassword);

        Task<IEnumerable<User>> GetUsersInRoleAsync(string role);

        Task AddUserToRoleAsync(User user, string role);

        Task RemoveUserFromRoleAsync(User user, string role);

        Task SetPhoneNumberAsync(User user, string phoneNumber);

        Task SetEmailAsync(User user, string email);

        Task SetUserNameAsync(User user, string userName);
    }
}
