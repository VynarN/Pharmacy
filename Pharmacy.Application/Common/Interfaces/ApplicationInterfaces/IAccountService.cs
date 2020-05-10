using Pharmacy.Application.Common.DTO.In;
using Pharmacy.Application.Common.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces
{
    public interface IAccountService
    {
        Task UpdateProfile(UserInDto model, string userId, string returnUrl);

        Task RegisterAsync(RegisterModel model, string returnUrl);

        Task<TokenModel> LoginAsync(LoginModel model);

        Task ConfirmEmailAsync(string userId, string token);

        Task ForgotPasswordAsync(string email, string returnUrl);

        Task ResetPasswordAsync(ResetPasswordModel model, string userId, string token);

        Task<IList<string>> GetUserRoles(string userId);

        Task DeleteProfile(string userId);
    }
}
