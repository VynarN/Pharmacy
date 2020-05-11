using Microsoft.AspNetCore.Http;
using Pharmacy.Application.Common.Interfaces.InfrastructureInterfaces;
using System.Security.Claims;

namespace Pharmacy.Api.Services
{
    public class CurrentUserService : ICurrentUser
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.Identity.Name;
        }

        public string UserId { get; }
    }
}
