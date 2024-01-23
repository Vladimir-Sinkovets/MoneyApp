using FluentValidation;

namespace MoneyApp.UseCases.Handlers.Users.Commands.RegisterUser
{
    public class RegisterUserValidation : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidation()
        {
            RuleFor(command => command.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(4);

            RuleFor(command => command.Email)
                .NotEmpty()
                .NotNull()
                .Matches("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");

            RuleFor(command => command.UserName)
                .NotEmpty()
                .NotNull();
        }
    }
}
