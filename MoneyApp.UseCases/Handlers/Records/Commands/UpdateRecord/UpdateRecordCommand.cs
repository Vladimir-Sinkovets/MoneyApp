using MediatR;

namespace MoneyApp.UseCases.Handlers.Records.Commands.UpdateRecord
{
    public class UpdateRecordCommand : IRequest<Guid>
    {
        public Guid RecordId { get; set; }
        public required string Text { get; set; }
        public required decimal Change { get; set; }
        public required DateTime Created { get; set; }
    }
}
