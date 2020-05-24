using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pharmacy.Api.Auxiliary;
using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using System;
using System.Threading.Tasks;

namespace Pharmacy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController: ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly ICookieService _cookieService;
        private readonly ILogger<TokenController> _logger;

        public TokenController(ITokenService tokenService, ILogger<TokenController> logger, IConfiguration configuration, ICookieService cookieService)
        {
            _tokenService = tokenService;
            _logger = logger;
            _config = configuration;
            _cookieService = cookieService;
        }

        [HttpPut("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var accessToken = _cookieService.GetCookieValue(_config["CookieSettings:AccessTokenCookieName"]);
                var refreshToken = _cookieService.GetCookieValue(_config["CookieSettings:RefreshTokenCookieName"]);

                var tokens = await _tokenService.RefreshToken(accessToken, refreshToken);

                _cookieService.RefreshCookie(tokens.AccessToken, tokens.RefreshToken);

                return Ok();
            }
            catch (ObjectException ex)
            {
                return Forbid(ex.ToString());
            }
            catch (Exception ex)
            {
                return ControllersAuxiliary.LogExceptionAndReturnError(ex, _logger, Response);
            }
        }
    }
}
