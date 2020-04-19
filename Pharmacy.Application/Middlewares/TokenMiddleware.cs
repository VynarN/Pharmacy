using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Pharmacy.Application.Common.Models;

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
            var cookieValue = context.Request.Cookies?[configuration["CookieConfiguration:TokenCookieName"]];
            if (!string.IsNullOrEmpty(cookieValue))
            {
                TokenModel deserializedTokens = JsonConvert.DeserializeObject<TokenModel>(cookieValue);
                context.Request.Headers.Add("Authorization", "Bearer " + deserializedTokens.AccessToken);
            }
            await _next.Invoke(context);
        }
    }
}
