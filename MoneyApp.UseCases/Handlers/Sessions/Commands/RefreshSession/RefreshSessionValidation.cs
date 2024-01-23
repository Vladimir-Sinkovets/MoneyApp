using FluentValidation;

namespace MoneyApp.UseCases.Handlers.Sessions.Commands.RefreshSession
{
    public class RefreshSessionValidation : AbstractValidator<RefreshSessionCommand>
    {
        public RefreshSessionValidation()
        {
            RuleFor(x => x.RefreshToken)
                .NotNull()
                .NotEmpty();
        }
    }
}
