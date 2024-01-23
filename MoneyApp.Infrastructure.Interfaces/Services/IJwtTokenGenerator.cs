using MoneyApp.Entities.Models;

namespace MoneyApp.Infrastructure.Interfaces.Services
{
    public interface IJwtTokenGenerator 
    {
        string Generate(User user);
    }
}
