using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MoneyApp.UseCases.Behaviour;
using MoneyApp.UseCases.Handlers.Users.Commands.RegisterUser;
using System.Reflection;

namespace MoneyApp.UseCases.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(RegisterUserCommand))!);
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
