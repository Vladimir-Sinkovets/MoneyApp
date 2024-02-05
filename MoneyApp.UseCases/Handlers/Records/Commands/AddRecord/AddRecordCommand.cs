using MediatR;

namespace MoneyApp.UseCases.Handlers.Records.Commands.AddRecord
{
    public class AddRecordCommand : IRequest<Guid>
    {
        public required string Text { get; set; }
        public required decimal Change { get; set; }
        public required DateTime Created { get; set; }
    }
}
