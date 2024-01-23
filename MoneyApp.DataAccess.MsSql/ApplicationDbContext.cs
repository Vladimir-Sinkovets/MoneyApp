using Microsoft.EntityFrameworkCore;
using MoneyApp.Entities.Models;
using MoneyApp.Infrastructure.Interfaces.DataAccess;

namespace MoneyApp.DataAccess.MsSql
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }

        async Task IDbContext.SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Id)
                .IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Password)
                .IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.Role)
                .IsRequired();

            modelBuilder.Entity<Session>().ToTable("Sessions");
            modelBuilder.Entity<Session>().HasKey(x => x.Id);
            modelBuilder.Entity<Session>().Property(x => x.Id)
                .IsRequired();
            modelBuilder.Entity<Session>().Property(x => x.App)
                .IsRequired();
            modelBuilder.Entity<Session>().Property(x => x.Expires)
                .IsRequired();
            modelBuilder.Entity<Session>().Property(x => x.Created)
                .IsRequired();
            modelBuilder.Entity<Session>().Property(x => x.RefreshToken)
                .IsRequired();
            modelBuilder.Entity<Session>()
                .HasOne(x => x.User)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
