using Microsoft.EntityFrameworkCore;
using MoneyApp.Entities.Models;

namespace MoneyApp.Infrastructure.Interfaces.DataAccess
{
    public interface IDbContext
    {
        DbSet<User> Users {  get; }
        DbSet<Session> Sessions {  get; }
        DbSet<Record> Records {  get; }
        Task SaveChangesAsync();
    }
}
