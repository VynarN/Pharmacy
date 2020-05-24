using Microsoft.IdentityModel.Tokens;
using Pharmacy.Application.Common.AppObjects;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Common.Interfaces.HelpersInterfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using Pharmacy.Application.Common.Validators;
using System;
using System.Threading.Tasks;

namespace Pharmacy.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserManager _userManager;

        public TokenService(ITokenHelper tokenHelper, IUserManager userManager)
        {
            _tokenHelper = tokenHelper;
            _userManager = userManager;
        }
        public async Task<Tokens> RefreshToken(string accessToken, string refreshToken)
        {
            StringArgumentValidator.ValidateStringArgument(accessToken, nameof(accessToken));
            StringArgumentValidator.ValidateStringArgument(refreshToken, nameof(refreshToken));

            var user = await _tokenHelper.GetUserFromExpiredToken(accessToken);

            if (!user.RefreshToken.Equals(refreshToken))
                throw new SecurityTokenException(ExceptionStrings.RefreshTokenException);

            var newAccessToken = await _tokenHelper.GenerateAccessToken(user);
            var newRefreshToken = _tokenHelper.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            await _userManager.UpdateAsync(user);

            return new Tokens() { AccessToken = newAccessToken, RefreshToken = newRefreshToken };
        }

        public Task Revoke(string userEmail)
        {
            throw new NotImplementedException();
        }
    }
}
