using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Common.Models;
using Pharmacy.Application.Helpers;
using Pharmacy.Domain.Entites;
using Pharmacy.Infrastructure.Common.Extensions;
using Pharmacy.Infrastructure.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Pharmacy.Application.Common.DTO.In;

namespace Pharmacy.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;
        private readonly IEmailSender _emailSender;

        public AccountService(UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           IMapper mapper,
                           IConfiguration configuration,
                           IHostingEnvironment environment,
                           IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _configuration = configuration;
            _emailSender = emailSender;
            _environment = environment;
        }

        public async Task RegisterAsync(RegisterModel model, string returnUrl)
        {
            var mappedUser = _mapper.Map<User>(model);

            mappedUser.NormalizedUserName = mappedUser.Email.ToUpper();
            mappedUser.UserName = mappedUser.Email;
            mappedUser.NormalizedUserName = mappedUser.NormalizedEmail.ToUpper();

            var createResult = await _userManager.CreateAsync(mappedUser, model.Password);

            if (createResult.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(mappedUser.Email.ToUpper());

                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var encodedToken = confirmEmailToken.Base64UrlEncodeString();

                var link = string.Format(_configuration["Server:ConfirmEmailEndpoint"], user.Id, encodedToken, returnUrl);

                await EmailHelper.SendEmail(user.Email, 
                       "EmailSettings:ConfirmEmailPathToTemplate", 
                       "EmailSettings:ConfirmEmailSubject",
                       _environment.WebRootPath, 
                       _configuration, 
                       _emailSender, 
                       link);
                     
            }
            throw new UserRegistrationException(ExceptionStrings.CreateUserException, mappedUser.Email);
        }

        public async Task<TokenModel> LoginAsync(LoginModel model)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (signInResult.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email.ToUpper());

                user.RefreshToken = TokenHelper.GenerateRefreshToken();

                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    var accessToken = await TokenHelper.GenerateAccessToken(user, _userManager, _configuration);
                    return new TokenModel() { AccessToken = accessToken, RefreshToken = user.RefreshToken };
                }
                throw new UserLoginException(ExceptionStrings.LoginException, user.Email);
            }
            throw new ObjectNotFoundException(ExceptionStrings.UserNotFoundException, model.Email);
        }

        public async Task ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new ObjectNotFoundException(ExceptionStrings.UserNotFoundException, userId);

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Count == 0)
            {
                await AddUserToDefaultRole(user);
            }

            var decodedToken = token.Base64UrlDecodeString();

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (!result.Succeeded)
                throw new ConfirmationException(ExceptionStrings.EmailConfirmException, user.Email);
        }

        public async Task ForgotPasswordAsync(string email, string returnUrl)
        {
            var user = await _userManager.FindByEmailAsync(email.ToUpper());
            if (user == null || !user.EmailConfirmed)
                throw new ObjectNotFoundException(ExceptionStrings.UserNotFoundException, email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedEmailToken = token.Base64UrlEncodeString();

            var link = $"{returnUrl}/{user.Id}/{encodedEmailToken}";

             await EmailHelper.SendEmail(user.Email, 
                   "EmailSettings:ResetPasswordPathToTemplate", 
                   "EmailSettings:ResetPasswordSubject",
                    _environment.WebRootPath, 
                    _configuration, 
                    _emailSender,
                    link);
        }          

        public async Task ResetPasswordAsync(ResetPasswordModel model, string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new ObjectNotFoundException(ExceptionStrings.UserNotFoundException, userId);

            var decodedToken = token.Base64UrlDecodeString();

            var resetPassResult = await _userManager.ResetPasswordAsync(user, decodedToken, model.Password);

            if (!resetPassResult.Succeeded)
                throw new ObjectUpdateException(ExceptionStrings.ResetPasswordException, user.Email);
        }

        private async Task<IdentityResult> AddUserToDefaultRole(User user)
        {
            var usersInRoles = await _userManager.GetUsersInRoleAsync(RoleStrings.Roles[3]);

            IdentityResult addedToRole;

            if (usersInRoles == null || usersInRoles.Count < 1)
            {
                addedToRole = await _userManager.AddToRoleAsync(user, RoleStrings.Roles[3]);
            }
            else
            {
                addedToRole = await _userManager.AddToRoleAsync(user, RoleStrings.Roles[0]);
            }

            if (!addedToRole.Succeeded)
                throw new RoleException(ExceptionStrings.AddToRoleException);
            return addedToRole;
        }

        public async Task<IList<string>> GetUserRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new ObjectNotFoundException(ExceptionStrings.UserNotFoundException, id);

            return await _userManager.GetRolesAsync(user);
        }

        public async Task UpdateProfile(UserInDto model, string userId, string returnUrl)
        {
            var userToBeUpdated = await _userManager.FindByIdAsync(userId);

            var changePhoneNumberResult = await _userManager.SetPhoneNumberAsync(userToBeUpdated, model.PhoneNumber);

            if (!changePhoneNumberResult.Succeeded)
                throw new ObjectUpdateException(ExceptionStrings.UserUpdateException, userToBeUpdated.Id, model.PhoneNumber);

            if (model.Email != userToBeUpdated.Email)
            {
                var changeEmailResult = await _userManager.SetEmailAsync(userToBeUpdated, model.Email);
                if (!changeEmailResult.Succeeded)
                {
                    throw new ObjectUpdateException(ExceptionStrings.UserUpdateException, userToBeUpdated.Id, model.Email);
                }
                else
                {
                    await _userManager.SetUserNameAsync(userToBeUpdated, model.Email);

                    var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(userToBeUpdated);

                    var encodedToken = confirmEmailToken.Base64UrlEncodeString();

                    var link = string.Format(_configuration["Server:ConfirmEmailEndpoint"], userId, encodedToken, returnUrl);

                    await EmailHelper.SendEmail(userToBeUpdated.Email, 
                        "EmailSettings:ConfirmEmailPathToTemplate", 
                        "EmailSettings:ConfirmEmailSubject",
                        _environment.WebRootPath,
                        _configuration, 
                        _emailSender, 
                        link);
                }
            }

            var updateUserResult = await _userManager.UpdateAsync(userToBeUpdated);

            if (!updateUserResult.Succeeded)
                throw new ObjectUpdateException(ExceptionStrings.UserUpdateException,userToBeUpdated.Id, model.FirstName + " " + model.SecondName);
        }

        public async Task DeleteProfile(string userId)
        {
            var tempUser = await _userManager.FindByIdAsync(userId);

            if (tempUser == null)
                throw new ObjectNotFoundException(ExceptionStrings.UserNotFoundException, userId);

            var result = await _userManager.DeleteAsync(tempUser);

            if (!result.Succeeded)
                throw new ObjectDeleteException(ExceptionStrings.DeleteUserException, userId);
        }
    }
}
