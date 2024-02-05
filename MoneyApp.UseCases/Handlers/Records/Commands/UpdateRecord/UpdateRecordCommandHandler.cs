using MediatR;
using MoneyApp.Infrastructure.Interfaces.DataAccess;
using MoneyApp.Infrastructure.Interfaces.Services;
using MoneyApp.UseCases.Exceptions;

namespace MoneyApp.UseCases.Handlers.Records.Commands.UpdateRecord
{
    public class UpdateRecordCommandHandler : IRequestHandler<UpdateRecordCommand, Guid>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public UpdateRecordCommandHandler(IDbContext dbContext, ICurrentUserAccessor currentUserAccessor)
        {
            _dbContext = dbContext;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<Guid> Handle(UpdateRecordCommand request, CancellationToken cancellationToken)
        {
            var user = _currentUserAccessor.GetCurrentUser();

            var record = _dbContext.Records
                .FirstOrDefault(r => r.Id == request.RecordId && r.UserId == user.Id);

            if (record == null)
                throw new RecordNotFoundException();

            record.Created = request.Created;
            record.Text = request.Text;
            record.Change = request.Change;

            await _dbContext.SaveChangesAsync();

            return record.Id;
        }
    }
}
