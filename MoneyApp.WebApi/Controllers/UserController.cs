using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyApp.Infrastructure.Interfaces.DataAccess;
using MoneyApp.UseCases.Handlers.Sessions.Commands.RefreshSession;
using MoneyApp.UseCases.Handlers.Users.Commands.LoginUser;
using MoneyApp.UseCases.Handlers.Users.Commands.RegisterUser;
using MoneyApp.WebApi.Models;
using System.Text;

namespace MoneyApp.WebApi.Controllers
{
    public class UserController : Controller
    {
        private const string RefreshTokenCookieKey = "refreshToken";
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody]RegisterUser user)
        {
            var command = new RegisterUserCommand()
            {
                Email = user.Email,
                Password = user.Password,
                UserName = user.UserName,
                App = user.App
            };

            var dto = await _mediator.Send(command);

            SetRefreshTokenCookie(dto.RefreshToken, dto.Expires);

            return Ok(new { Token = dto.JwtToken, });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody]LoginUser user)
        {
            var query = new LoginUserCommand()
            {
                Password = user.Password,
                UserName = user.UserName,
                App = user.App,
            };

            var token = await _mediator.Send(query);

            SetRefreshTokenCookie(token.RefreshToken, token.RefreshTokenExpires);

            return Ok(new { Token = token.JwtToken, });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var request = new RefreshSessionCommand()
            {
                RefreshToken = HttpContext.Request.Cookies[RefreshTokenCookieKey]!,
            };

            var dto = await _mediator.Send(request);

            SetRefreshTokenCookie(dto.RefreshToken, dto.Expires);

            return Ok(new { Token = dto.Jwt, });
        }

        private void SetRefreshTokenCookie(string token, DateTime expires)
        {
            HttpContext.Response.Cookies.Append(
                key: RefreshTokenCookieKey,
                value: token,
                new CookieOptions()
                {
                    HttpOnly = true,
                    Expires = expires,
                    Path = "/api/refreshToken",
                });
        }
    }
}
