using MediatR;

namespace MoneyApp.UseCases.Handlers.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<RegisterUserDto>
    {
        public required string UserName { get; set; }
        public required string App { get; set; }
        public required string Password { get; set; } 
        public required string Email { get; set; }
    }
}
