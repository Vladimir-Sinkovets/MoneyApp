using MoneyApp.Infrastructure.Interfaces.Services;
using System.Security.Cryptography;

namespace MoneyApp.Infrastructure.Implementation.Services
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}
