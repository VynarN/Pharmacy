using Microsoft.Extensions.Configuration;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Interfaces.HelpersInterfaces;
using Pharmacy.Application.Common.Extensions;
using System.Linq;
using System;
using Pharmacy.Application.Common.DTO.In.Auth.Register;
using Pharmacy.Application.Common.DTO.In.Auth;
using Pharmacy.Application.Common.DTO.In.Auth.ResetPassword;
using Pharmacy.Application.Common.DTO.In.UserIn;
using Pharmacy.Application.Common.AppObjects;

namespace Pharmacy.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserManager _userManager;
        private readonly ISignInManager _signInManager;
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly IEmailHelper _emailHelper;
        private readonly ITokenHelper _tokenHelper;
        private readonly IRepository<User> _repository;

        public AccountService(IUserManager userManager,
                           IUserHelper userHelper,
                           ISignInManager signInManager,
                           IConfiguration configuration,
                           IEmailHelper emailHelper,
                           ITokenHelper tokenHelper,
                           IRepository<User> repository)
        {
            _userManager = userManager;
            _userHelper = userHelper;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailHelper = emailHelper;
            _tokenHelper = tokenHelper;
            _repository = repository;
        }

        public async Task RegisterAsync(RegisterDto model, string returnUrl)
        {
            var correctedUserEmail = model?.Email?.ToLower();

            var normalizedUserEmail = model?.Email?.ToUpper();
            
            var newUser = new User
            {
                FirstName = model?.FirstName,
                SecondName = model?.SecondName,
                Email = correctedUserEmail,
                NormalizedEmail = normalizedUserEmail,
                UserName = correctedUserEmail,
                NormalizedUserName = normalizedUserEmail,
                PhoneNumber = model?.PhoneNumber
            };

            using (var transaction = await _repository.BeginTransactionAsync())
            {
                await _userManager.CreateUserAsync(newUser, model?.Password);

                try
                {
                    var user = await _userHelper.FindUserByEmailAsync(newUser.Email);

                    var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var encodedToken = confirmEmailToken.Base64UrlEncodeString();

                    var link = string.Format(_configuration["Server:ConfirmEmailEndpoint"], user.Id, encodedToken, returnUrl);

                    await _emailHelper.Send(user.Email,
                           "EmailSettings:ConfirmEmailPathToTemplate",
                           "EmailSettings:ConfirmEmailSubject",
                            link);

                    await _repository.CommitTransactionAsync(transaction);
                }
                catch (Exception)
                {
                   await _repository.RollbackTransactionAsync(transaction);
                   throw;
                }
            }
        }

        public async Task<Tokens> LoginAsync(LoginDto model)
        {
            if (await _signInManager.SignInAsync(model.Email, model.Password))
            {
                var user = await _userHelper.FindUserByEmailAsync(model.Email);

                user.RefreshToken = _tokenHelper.GenerateRefreshToken();

                await _userManager.UpdateAsync(user);

                var accessToken = await _tokenHelper.GenerateAccessToken(user);

                return new Tokens() { AccessToken = accessToken, RefreshToken = user.RefreshToken };
            }

            throw new UserLoginException(ExceptionStrings.LoginException, model.Email);
        }

        public async Task ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userHelper.FindUserByIdAsync(userId);

            var userRoles = await _userManager.GetUserRolesAsync(user);

            if (userRoles.Count == 0)
                await AddUserToDefaultRole(user);
            
            var decodedToken = token.Base64UrlDecodeString();

            if (!await _userManager.ConfirmEmailAsync(user, decodedToken))
                throw new ConfirmationException(ExceptionStrings.EmailConfirmException, userId);
        }

        public async Task ForgotPasswordAsync(string email, string returnUrl)
        {
            var user = await _userHelper.FindUserByEmailAsync(email);

            if (!user.EmailConfirmed)
                throw new ObjectNotFoundException(ExceptionStrings.UserNotFoundException, email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedEmailToken = token.Base64UrlEncodeString();

            var link = string.Format(_configuration["Server:ForgotPasswordEndpoint"], returnUrl, user.Id, encodedEmailToken);

             await _emailHelper.Send(user.Email, 
                   "EmailSettings:ResetPasswordPathToTemplate", 
                   "EmailSettings:ResetPasswordSubject",
                    link);
        }          

        public async Task ResetPasswordAsync(ResetPasswordDto model, string userId, string token)
        {
            var user = await _userHelper.FindUserByIdAsync(userId);

            var decodedToken = token.Base64UrlDecodeString();

            if (!await _userManager.ResetPasswordAsync(user, decodedToken, model.Password))
                throw new ConfirmationException(ExceptionStrings.ResetPasswordException, userId);
        }

        public async Task<IList<string>> GetUserRoles(string userEmail)
        {
            var user = await _userHelper.FindUserByEmailAsync(userEmail);

            return await _userManager.GetUserRolesAsync(user);
        }

        public async Task UpdateProfile(UserInDto model, string userId, string returnUrl)
        {
            var userToBeUpdated = await _userHelper.FindUserByIdAsync(userId);

            await _userManager.SetPhoneNumberAsync(userToBeUpdated, model.PhoneNumber);

            if (model.Email != userToBeUpdated.Email)
            {
                using (var transaction = await _repository.BeginTransactionAsync())
                {
                    await _userManager.SetEmailAsync(userToBeUpdated, model.Email);
                    await _userManager.SetUserNameAsync(userToBeUpdated, model.Email);

                    var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(userToBeUpdated);
                    try
                    {
                        var encodedToken = confirmEmailToken.Base64UrlEncodeString();

                        var link = string.Format(_configuration["Server:ConfirmEmailEndpoint"], userId, encodedToken, returnUrl);

                        await _emailHelper.Send(userToBeUpdated.Email,
                            "EmailSettings:ConfirmEmailPathToTemplate",
                            "EmailSettings:ConfirmEmailSubject",
                                link);

                        await _repository.CommitTransactionAsync(transaction);
                    }
                    catch (Exception)
                    {
                        await _repository.RollbackTransactionAsync(transaction);
                        throw;
                    }
                }
            }
            
            userToBeUpdated.FirstName = model.FirstName;
            userToBeUpdated.SecondName = model.SecondName;
            
            await _userManager.UpdateAsync(userToBeUpdated);

        }

        public async Task DeleteProfile(string userId)
        {
            var user = await _userHelper.FindUserByIdAsync(userId);

             await _userManager.DeleteAsync(user);
        }

        private async Task AddUserToDefaultRole(User user)
        {
            var usersInRoles = (await _userManager.GetUsersInRoleAsync(RoleConstants.MainAdmin)).ToList();

            if (usersInRoles == null || usersInRoles.Count < 1)
            {
                await _userManager.AddUserToRoleAsync(user, RoleConstants.MainAdmin);
            }
            else
            {
                await _userManager.AddUserToRoleAsync(user, RoleConstants.User);
            }
        }
    }
}
