using MediatR;
using MoneyApp.Infrastructure.Interfaces.DataAccess;
using MoneyApp.Infrastructure.Interfaces.Services;
using MoneyApp.UseCases.Exceptions;

namespace MoneyApp.UseCases.Handlers.Records.Queries.GetRecord
{
    public class GetRecordQueryHandler : IRequestHandler<GetRecordQuery, GetRecordDto>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public GetRecordQueryHandler(IDbContext dbContext, ICurrentUserAccessor currentUserAccessor)
        {
            _dbContext = dbContext;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<GetRecordDto> Handle(GetRecordQuery request, CancellationToken cancellationToken)
        {
            var user = _currentUserAccessor.GetCurrentUser();

            var record = _dbContext.Records
                .FirstOrDefault(r => r.Id == request.RecordId && r.UserId == user.Id);

            if (record == null)
                throw new RecordNotFoundException();

            var dto = new GetRecordDto()
            {
                Change = record.Change,
                Created = record.Created,
                Id = record.Id,
                Text = record.Text,
            };

            return dto;
        }
    }
}
