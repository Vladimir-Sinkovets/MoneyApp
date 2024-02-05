using Microsoft.EntityFrameworkCore;
using MoneyApp.Entities.Models;
using MoneyApp.Infrastructure.Interfaces.DataAccess;

namespace MoneyApp.DataAccess.MsSql
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Record> Records { get; set; }

        async Task IDbContext.SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.ToTable("Users");
                user.HasKey(x => x.Id);
                user.Property(x => x.Id)
                    .IsRequired();
                user.Property(x => x.Password)
                    .IsRequired();
                user.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(50);
                user.Property(x => x.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
                user.Property(x => x.Role)
                    .IsRequired();
            });

            modelBuilder.Entity<Session>(session =>
            {
                session.ToTable("Sessions");
                session.HasKey(x => x.Id);
                session.Property(x => x.Id)
                    .IsRequired();
                session.Property(x => x.App)
                    .IsRequired();
                session.Property(x => x.Expires)
                    .IsRequired();
                session.Property(x => x.Created)
                    .IsRequired();
                session.Property(x => x.RefreshToken)
                    .IsRequired();
                session
                    .HasOne(x => x.User)
                    .WithMany(x => x.Sessions)
                    .HasForeignKey(x => x.UserId);
            });

            modelBuilder.Entity<Record>(record =>
            {
                record.ToTable("Records");
                record.HasKey(x => x.Id);
                record.Property(x => x.Id)
                    .IsRequired();
                record.Property(x => x.Change)
                    .HasPrecision(18, 4);
                record.Property(x => x.Created)
                    .IsRequired();
                record.Property(x => x.Text)
                    .HasMaxLength(500);
                record.Property(x => x.UserId)
                    .IsRequired();
                record
                    .HasOne(x => x.User)
                    .WithMany(x => x.Records)
                    .HasForeignKey(x => x.UserId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
