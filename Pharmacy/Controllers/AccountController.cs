using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmacy.Application.Common.Constants;
using Pharmacy.Application.Common.DTO.In;
using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Common.Interfaces.HelpersInterfaces;
using Pharmacy.Application.Common.Models;

namespace Pharmacy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly ICookieHelper _cookieHelper;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService service,ICookieHelper cookieHelper, ILogger<AccountController> logger)
        {
            _service = service;
            _cookieHelper = cookieHelper;
            _logger = logger;
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult> Update(UserInDto user, string returnUrl)
        {
            try
            {
                var userId = User.Identity.Name;

                await _service.UpdateProfile(user, userId, returnUrl);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, ex.Message);

                return BadRequest(ExceptionStrings.Exception);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel user)
        {
            try
            {
                var tokens = await _service.LoginAsync(user);

                var userRoles = await _service.GetUserRoles(user.Email);

                _cookieHelper.CreateCookie(user.RememberMe, tokens.AccessToken, tokens.RefreshToken);

                return Ok(userRoles);
            }
            catch (UserLoginException ex)
            {
                return BadRequest(ex.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, ex.Message);

                return BadRequest(ExceptionStrings.Exception);
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                _cookieHelper.CleanCookies();

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex, ex.Message);

                return BadRequest(ExceptionStrings.Exception);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model, string returnUrl)
        {
            try
            {
                await _service.RegisterAsync(model, returnUrl);

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, ex.Message);

                return BadRequest(ExceptionStrings.Exception);
            }
        }

        [HttpGet("confirm")]
        public async Task ConfirmEmail(string userId, string token, string returnUrl)
        {
            try
            {
                await _service.ConfirmEmailAsync(userId, token);

                Response.Redirect(returnUrl);
            }
            catch (ObjectNotFoundException ex)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;

                await Response.WriteAsync(ex.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, ex.Message); 

                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                await Response.WriteAsync(ExceptionStrings.Exception);
            }
        }

        [HttpPost("forgot/{email}")]
        public async Task<IActionResult> ForgotPassword(string email, string returnUrl)
        {
            try
            {
                await _service.ForgotPasswordAsync(email, returnUrl);

                return Accepted();
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.ToString());
            }
            catch(SendEmailException ex)
            {
                return BadRequest(ex.ToString());
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex, ex.Message);

                return BadRequest(ExceptionStrings.Exception);
            }
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model, string userId, string token)
        {
            try
            {
                await _service.ResetPasswordAsync(model, userId, token);

                return Ok();
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, ex.Message);

                return BadRequest(ExceptionStrings.Exception);
            }
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<ActionResult> Delete()
        {
            try
            {
                var userId = User.Identity.Name;

                await _service.DeleteProfile(userId);

                return Ok();
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, ex.Message);

                return BadRequest(ExceptionStrings.Exception);
            }
        }
    }
}