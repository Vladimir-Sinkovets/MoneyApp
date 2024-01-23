using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MoneyApp.Infrastructure.Implementation.Options;
using MoneyApp.UseCases.DependencyInjection;
using System.Text;
using MoneyApp.DataAccess.MsSql;
using MoneyApp.Infrastructure.Implementation.DependencyInjection;
using MoneyApp.Infrastructure.Interfaces.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace MoneyApp.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
                    };
                });

            builder.Services.Configure<JwtTokenOptions>(opt =>
            {
                opt.Issuer = configuration["Jwt:Issuer"]!;
                opt.Audience = configuration["Jwt:Audience"]!;
                opt.Key = configuration["Jwt:Key"]!;
            });

            builder.Services.AddDbContext<IDbContext, ApplicationDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("MsSqlConnection")!));
            
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddServices(); 
            builder.Services.AddUseCases();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}