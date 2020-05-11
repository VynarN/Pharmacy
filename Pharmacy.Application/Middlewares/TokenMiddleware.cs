using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Pharmacy.Application.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
        {
            var cookieValue = context.Request.Cookies?[configuration["CookieSettings:AccessTokenCookieName"]];

            if (!string.IsNullOrEmpty(cookieValue))
            {
                context.Request.Headers.Add("Authorization", "Bearer " + cookieValue);
            }
            await _next.Invoke(context);
        }
    }
}
