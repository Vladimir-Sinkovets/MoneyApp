using MediatR;

namespace MoneyApp.UseCases.Handlers.Records.Queries.GetRecord
{
    public class GetRecordQuery : IRequest<GetRecordDto>
    {
        public Guid RecordId { get; set; }
    }
}
