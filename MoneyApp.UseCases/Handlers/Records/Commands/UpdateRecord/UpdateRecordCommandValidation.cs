using FluentValidation;

namespace MoneyApp.UseCases.Handlers.Records.Commands.UpdateRecord
{
    public class UpdateRecordCommandValidation : AbstractValidator<UpdateRecordCommand>
    {
        public UpdateRecordCommandValidation()
        {
            RuleFor(x => x.Text)
                .NotNull();
        }
    }
}
