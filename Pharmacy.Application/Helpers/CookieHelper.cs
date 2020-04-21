using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pharmacy.Application.Common.Models;
using System;

namespace Pharmacy.Application.Helpers
{
    public static class CookieHelper
    {
        public static void CreateCookie(IConfiguration configuration, HttpResponse response, bool isPersistent, TokenModel token)
        {
            CleanCookies(configuration, response);
            var CookieOptions = new CookieOptions() { HttpOnly = true, Secure = true, IsEssential = true, SameSite = SameSiteMode.Strict};
            if (isPersistent)
            {
                CookieOptions.MaxAge = TimeSpan.FromDays(Convert.ToDouble(configuration["CookieSettings:ExpireMinutes"]));
                response.Cookies.Append(configuration["CookieSettings:TokenCookieName"], JsonConvert.SerializeObject(token), CookieOptions);
                response.Cookies.Append(configuration["CookieSettings:IsPersistentCookieName"], "true");
            }
            else
            {
                response.Cookies.Append(configuration["CookieSettings:TokenCookieName"], JsonConvert.SerializeObject(token), CookieOptions);
            }
            response.Headers.Add("X-Content-Type-Options", "nosniff");
            response.Headers.Add("X-Xss-Protection", "1");
            response.Headers.Add("X-Frame-Options", "DENY");

        }

        public static void RefreshCookie(IConfiguration configuration, HttpRequest request, HttpResponse response, TokenModel token)
        {
            var isPersistent = request.Cookies.ContainsKey(configuration["CookieSettings:IsPersistentCookieName"]);
            CreateCookie(configuration, response, isPersistent, token);
        }

        public static TokenModel GetTokenValue(IConfiguration configuration, HttpRequest request)
        {
            var cookieValue = request.Cookies[configuration["CookieSettings:TokenCookieName"]];
            if (!string.IsNullOrEmpty(cookieValue))
            {
                return JsonConvert.DeserializeObject<TokenModel>(cookieValue);
            }
            return null;
        }

        public static void CleanCookies(IConfiguration configuration, HttpResponse response)
        {
            response.Cookies.Delete(configuration["CookieSettings:IsPersistentCookieName"]);
            response.Cookies.Delete(configuration["CookieSettings:TokenCookieName"]);
        }
    }
}
