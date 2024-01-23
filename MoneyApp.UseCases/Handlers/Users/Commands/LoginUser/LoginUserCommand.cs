using MediatR;

namespace MoneyApp.UseCases.Handlers.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<LoginUserDto>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string App { get; set; }
    }
}