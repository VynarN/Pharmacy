using Pharmacy.Application.Common.AppObjects;
using Pharmacy.Application.Common.DTO.In.Auth;
using Pharmacy.Application.Common.DTO.In.Auth.Register;
using Pharmacy.Application.Common.DTO.In.Auth.ResetPassword;
using Pharmacy.Application.Common.DTO.In.UserIn;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces
{
    public interface IAccountService
    {
        Task UpdateProfile(UserInDto model, string userId, string returnUrl);

        Task RegisterAsync(RegisterDto model, string returnUrl);

        Task<Tokens> LoginAsync(LoginDto model);

        Task ConfirmEmailAsync(string userId, string token);

        Task ForgotPasswordAsync(string email, string returnUrl);

        Task ResetPasswordAsync(ResetPasswordDto model, string userId, string token);

        Task<IList<string>> GetUserRoles(string userEmail);

        Task DeleteProfile(string userId);
    }
}
