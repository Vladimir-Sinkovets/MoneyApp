using FluentValidation;

namespace MoneyApp.UseCases.Handlers.Records.Queries.GetRecord
{
    public class GetRecordQueryValidation : AbstractValidator<GetRecordQuery>
    {
        public GetRecordQueryValidation()
        {
            RuleFor(x => x.RecordId)
                .NotEmpty();
        }
    }
}
