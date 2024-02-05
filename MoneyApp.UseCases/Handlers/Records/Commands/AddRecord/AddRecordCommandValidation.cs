using FluentValidation;

namespace MoneyApp.UseCases.Handlers.Records.Commands.AddRecord
{
    public class AddRecordCommandValidation : AbstractValidator<AddRecordCommand>
    {
        public AddRecordCommandValidation()
        {
            RuleFor(x => x.Text)
                .NotNull();
        }
    }
}
