using MoneyApp.Entities.Models;

namespace MoneyApp.Infrastructure.Interfaces.Services
{
    public interface ICurrentUserAccessor
    {
        User? GetCurrentUser();
    }
}
