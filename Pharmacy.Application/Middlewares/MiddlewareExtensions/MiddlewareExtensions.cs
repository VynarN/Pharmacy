using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;

namespace Pharmacy.Application.Middlewares.MiddlewareExtensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseXsrfProtection(this IApplicationBuilder builder, IAntiforgery antiforgery)
               => builder.UseMiddleware<XsrfProtectionMiddleware>(antiforgery);
    }
}
