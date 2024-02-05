using MediatR;
using MoneyApp.Infrastructure.Interfaces.DataAccess;
using MoneyApp.Infrastructure.Interfaces.Services;
using MoneyApp.UseCases.Exceptions;

namespace MoneyApp.UseCases.Handlers.Records.Commands.DeleteRecord
{
    public class DeleteRecordCommandHandler : IRequestHandler<DeleteRecordCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserAccessor _userAccessor;

        public DeleteRecordCommandHandler(IDbContext dbContext, ICurrentUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }

        public async Task Handle(DeleteRecordCommand request, CancellationToken cancellationToken)
        {
            var user = _userAccessor.GetCurrentUser();

            var record = _dbContext.Records
                .FirstOrDefault(r => r.Id == request.RecordId && r.UserId == user.Id);

            if (record == null)
                throw new RecordNotFoundException();

            _dbContext.Records.Remove(record);

            await _dbContext.SaveChangesAsync();
        }
    }
}
