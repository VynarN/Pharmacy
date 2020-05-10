using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Pharmacy.Application.Common.Interfaces.HelpersInterfaces;
using System;

namespace Pharmacy.Application.Helpers
{
    public class CookieHelper: ICookieHelper
    {
        private readonly IConfiguration _configuration;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public void CreateCookie(bool isPersistent, string accessToken, string refreshToken)
        {
            var CookieOptions = new CookieOptions() { HttpOnly = true, Secure = true, IsEssential = true, SameSite = SameSiteMode.Strict };

            if (isPersistent)
            {
                CookieOptions.MaxAge = TimeSpan.FromDays(Convert.ToDouble(_configuration["CookieSettings:ExpireMinutes"]));

                _httpContextAccessor.HttpContext.Response.Cookies.Append(_configuration["CookieSettings:AccessTokenCookieName"], accessToken, CookieOptions);
                _httpContextAccessor.HttpContext.Response.Cookies.Append(_configuration["CookieSettings:RefreshTokenCookieName"], refreshToken, CookieOptions);
                _httpContextAccessor.HttpContext.Response.Cookies.Append(_configuration["CookieSettings:IsPersistentCookieName"], "true");
            }
            else
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Append(_configuration["CookieSettings:AccessTokenCookieName"], accessToken, CookieOptions);
                _httpContextAccessor.HttpContext.Response.Cookies.Append(_configuration["CookieSettings:RefreshTokenCookieName"], refreshToken, CookieOptions);
            }
        }

        public void RefreshCookie(string accessToken, string refreshToken)
        {
            var isPersistent = _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(_configuration["CookieSettings:IsPersistentCookieName"]);
            CreateCookie(isPersistent, accessToken, refreshToken);
        }

        public string GetCookieValue(string cookieName)
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[cookieName];
        }

        public void CleanCookies()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(_configuration["CookieSettings:IsPersistentCookieName"]);
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(_configuration["CookieSettings:AccessTokenCookieName"]);
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(_configuration["CookieSettings:RefreshTokenCookieName"]);
        }
    }
}
