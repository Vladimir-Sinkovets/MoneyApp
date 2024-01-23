using Microsoft.AspNetCore.Http;
using MoneyApp.Entities.Models;
using MoneyApp.Infrastructure.Implementation.Common;
using MoneyApp.Infrastructure.Interfaces.Services;
using System.Security.Claims;

namespace MoneyApp.Infrastructure.Implementation.Services
{
    public class CurrentUserAccessor : ICurrentUserAccessor
    {
        private readonly HttpContext _httpContext;

        public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public User? GetCurrentUser()
        {
            var identity = _httpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var claims = identity.Claims;

                var user = new User()
                {
                    Email = claims.FirstOrDefault(c => c.Type == ClaimTitles.Email)?.Value ?? string.Empty,
                    Role = claims.FirstOrDefault(c => c.Type == ClaimTitles.Role)?.Value ?? string.Empty,
                    UserName = claims.FirstOrDefault(c => c.Type == ClaimTitles.UserName)?.Value ?? string.Empty,
                };

                return user;
            }

            return null;
        }
    }
}
