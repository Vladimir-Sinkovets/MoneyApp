using MoneyApp.Infrastructure.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using MoneyApp.Infrastructure.Implementation.Options;
using Microsoft.Extensions.Options;
using MoneyApp.Entities.Models;
using MoneyApp.Infrastructure.Implementation.Common;

namespace MoneyApp.Infrastructure.Implementation.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtTokenOptions _options;

        public JwtTokenGenerator(IOptions<JwtTokenOptions> options)
        {
            _options = options.Value;
        }

        public string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(ClaimTitles.UserName, user.UserName),
                new Claim(ClaimTitles.Email, user.Email),
                new Claim(ClaimTitles.Role, user.Role),
            };

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
