using MediatR;

namespace MoneyApp.UseCases.Handlers.Records.Commands.DeleteRecord
{
    public class DeleteRecordCommand : IRequest
    {
        public required Guid RecordId { get; set; }
    }
}
