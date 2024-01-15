using Microsoft.AspNetCore.Mvc;
using MoneyApp.WebApi.Models;
using System.Security.Claims;

namespace MoneyApp.WebApi.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return Ok("1");
        }

        private User? GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var claims = identity.Claims;
                
                var user = new User()
                {
                    Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty,
                    Role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? string.Empty,
                    UserName = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                };

                return user;
            }

            return null;
        }
    }
}
