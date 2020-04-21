using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pharmacy.Application.Common.DTO.In;
using Pharmacy.Application.Common.Exceptions;
using Pharmacy.Application.Common.Interfaces;
using Pharmacy.Application.Common.Models;
using Pharmacy.Application.Helpers;
using Pharmacy.Infrastructure.Common.Exceptions;

namespace Pharmacy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
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
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ObjectUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (IOException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SendEmailException ex)
            {
                return BadRequest(ex.Message + ex.EmailReceiver + " " + ex.EmailSender);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel user)
        {
            try
            {
                var tokens = await _service.LoginAsync(user);
                var userRoles = await _service.GetUserRoles(user.Email);
                CookieHelper.CreateCookie(_configuration, HttpContext.Response, user.RememberMe, tokens);
                return Ok(userRoles);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message + ex.ObjectIdentifier);
            }
            catch (UserLoginException ex)
            {
                return BadRequest(ex.Message + ex.ObjectIdentifier);
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                CookieHelper.CleanCookies(_configuration, HttpContext.Response);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
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
            catch (UserRegistrationException ex)
            {
                return BadRequest(ex.Message + ex.ObjectIdentifier);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (IOException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SendEmailException ex)
            {
                return BadRequest(ex.Message + ex.EmailReceiver + " " + ex.EmailSender);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                await Response.WriteAsync(ex.Message + ex.ObjectIdentifier);
            }
            catch (ObjectUpdateException ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await Response.WriteAsync(ex.Message + ex.ObjectIdentifier + ex.UpdateValue);
            }
            catch (ArgumentException ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await Response.WriteAsync(ex.Message);
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
                return NotFound(ex.Message + ex.ObjectIdentifier);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message + ex.ParamName);
            }
            catch(IOException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(SendEmailException ex)
            {
                return BadRequest(ex.Message + ex.EmailReceiver + " " + ex.EmailSender);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
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
                return NotFound(ex.Message + ex.ObjectIdentifier);
            }
            catch (ObjectUpdateException ex)
            {
                return BadRequest(ex.Message + ex.ObjectIdentifier + ex.UpdateValue);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message + " " + ex.ParamName);
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
                return NotFound(ex.Message + ex.ObjectIdentifier);
            }
            catch (ObjectDeleteException ex)
            {
                return BadRequest(ex.Message + ex.ObjectIdentifier);
            }
        }
    }
}