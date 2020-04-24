using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Pharmacy.Application.Middlewares
{
    public class XsrfProtectionMiddleware
    {
        private readonly IAntiforgery _antiforgery;
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;

        public XsrfProtectionMiddleware(RequestDelegate next, IAntiforgery antiforgery, IConfiguration configuration)
        {
            _next = next;
            _antiforgery = antiforgery;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var expiresIn = Convert.ToDouble(_configuration["CookieSettings:ExpireMinutes"]);

            context.Response.Cookies.Append(
                _configuration["CookieSettings:XsrfCookieName"],
                _antiforgery.GetAndStoreTokens(context).RequestToken,
                new CookieOptions { HttpOnly = false, Secure = true, MaxAge = TimeSpan.FromMinutes(expiresIn) });

            await _next(context);
        }
    }
}
