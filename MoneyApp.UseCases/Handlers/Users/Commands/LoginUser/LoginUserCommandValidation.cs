using FluentValidation;

namespace MoneyApp.UseCases.Handlers.Users.Commands.LoginUser
{
    public class LoginUserCommandValidation : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidation()
        {
            RuleFor(query => query.Password)
                .NotEmpty()
                .NotNull();

            RuleFor(query => query.UserName)
                .NotEmpty()
                .NotNull();
        }
    }
}
