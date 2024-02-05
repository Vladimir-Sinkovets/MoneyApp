using MediatR;
using MoneyApp.Entities.Models;
using MoneyApp.Infrastructure.Interfaces.DataAccess;
using MoneyApp.Infrastructure.Interfaces.Services;

namespace MoneyApp.UseCases.Handlers.Records.Commands.AddRecord
{
    public class AddRecordCommandHandler : IRequestHandler<AddRecordCommand, Guid>
    {
        private readonly IDbContext _dbContext;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public AddRecordCommandHandler(IDbContext dbContext, ICurrentUserAccessor currentUserAccessor)
        {
            _dbContext = dbContext;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<Guid> Handle(AddRecordCommand request, CancellationToken cancellationToken)
        {
            var record = new Record()
            {
                Created = request.Created,
                User = _currentUserAccessor.GetCurrentUser(),
                Change = request.Change,
                Text = request.Text,
                Id = Guid.NewGuid(),
            };

            _dbContext.Records.Add(record);

            await _dbContext.SaveChangesAsync();

            return record.Id;
        }
    }
}