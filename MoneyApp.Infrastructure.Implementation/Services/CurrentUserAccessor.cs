using Microsoft.AspNetCore.Http;
using MoneyApp.Entities.Models;
using MoneyApp.Infrastructure.Implementation.Common;
using MoneyApp.Infrastructure.Interfaces.DataAccess;
using MoneyApp.Infrastructure.Interfaces.Exceptions;
using MoneyApp.Infrastructure.Interfaces.Services;
using System.Security.Claims;

namespace MoneyApp.Infrastructure.Implementation.Services
{
    public class CurrentUserAccessor : ICurrentUserAccessor
    {
        private readonly HttpContext _httpContext;
        private readonly IDbContext _dbContext;

        public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor, IDbContext dbContext)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _dbContext = dbContext;
        }

        public User GetCurrentUser()
        {
            var identity = _httpContext.User.Identity as ClaimsIdentity
                ?? throw new UserUnauthorizedException();
            
            var claims = identity.Claims;

            var email = claims.FirstOrDefault(c => c.Type == ClaimTitles.Email)?.Value;

            var user = _dbContext.Users
                .FirstOrDefault(x => x.Email == email);

            if (user == null) 
                throw new UserUnauthorizedException();
            
            return user;

            //var user = new User()
            //{
            //    Email = claims.FirstOrDefault(c => c.Type == ClaimTitles.Email)?.Value ?? string.Empty,
            //    Role = claims.FirstOrDefault(c => c.Type == ClaimTitles.Role)?.Value ?? string.Empty,
            //    UserName = claims.FirstOrDefault(c => c.Type == ClaimTitles.UserName)?.Value ?? string.Empty,
            //};
        }
    }
}
