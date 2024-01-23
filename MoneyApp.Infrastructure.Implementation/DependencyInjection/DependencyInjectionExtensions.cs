using Microsoft.Extensions.DependencyInjection;
using MoneyApp.Infrastructure.Implementation.Services;
using MoneyApp.Infrastructure.Interfaces.Services;

namespace MoneyApp.Infrastructure.Implementation.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddTransient<ICurrentUserAccessor, CurrentUserAccessor>();
            services.AddTransient<IRefreshTokenGenerator, RefreshTokenGenerator>();

            return services;
        }
    }
}
