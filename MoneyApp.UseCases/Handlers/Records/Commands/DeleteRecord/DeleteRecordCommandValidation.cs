using FluentValidation;

namespace MoneyApp.UseCases.Handlers.Records.Commands.DeleteRecord
{
    public class DeleteRecordCommandValidation : AbstractValidator<DeleteRecordCommand>
    {
        public DeleteRecordCommandValidation()
        {
            RuleFor(x => x.RecordId)
                .NotEmpty()
                .NotEqual(Guid.Empty);
        }
    }
}
