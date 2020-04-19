using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common.Interfaces
{
    public interface IAccountService
    {
        //Task UpdateProfile(UserDto model, string returnUrl);

        //Task RegisterAsync(UserRegistrationDto model, string returnUrl);

        //Task<UserTokenDto> LoginAsync(UserLoginDto model);

        //Task ConfirmEmailAsync(string userId, string token);

        //Task ForgotPasswordAsync(ResetPasswordDto model, string returnUrl);

        //Task ResetPasswordAsync(ResetPasswordConfirmDto model);

        //Task<IList<string>> GetUserRoles(string userEmail);

        Task DeleteProfile(string userId);
    }
}
