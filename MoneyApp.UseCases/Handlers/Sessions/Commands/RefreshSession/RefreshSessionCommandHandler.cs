using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneyApp.Entities.Models;
using MoneyApp.Infrastructure.Interfaces.DataAccess;
using MoneyApp.Infrastructure.Interfaces.Services;
using MoneyApp.UseCases.Exceptions;

namespace MoneyApp.UseCases.Handlers.Sessions.Commands.RefreshSession
{
    public class RefreshSessionCommandHandler : IRequestHandler<RefreshSessionCommand, RefreshSessionCommandDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public RefreshSessionCommandHandler(
            IDbContext dbContext,
            IRefreshTokenGenerator refreshTokenGenerator,
            IJwtTokenGenerator jwtTokenGenerator,
            ICurrentUserAccessor currentUserAccessor)
        {
            _dbContext = dbContext;
            _refreshTokenGenerator = refreshTokenGenerator;
            _jwtTokenGenerator = jwtTokenGenerator;
            _currentUserAccessor = currentUserAccessor;
        }

        public async Task<RefreshSessionCommandDto> Handle(RefreshSessionCommand request, CancellationToken cancellationToken)
        {
            var session = _dbContext.Sessions
                .Include(s => s.User)
                .FirstOrDefault(s => s.RefreshToken == request.RefreshToken);

            if (session == null)
            {
                throw new SessionNotFoundException();
            }

            var user = session.User;
            
            if (session.Expires < DateTime.Now)
            {
                _dbContext.Sessions.Remove(session);

                throw new SessionNotFoundException();
            }

            var jwt = _jwtTokenGenerator.Generate(user);

            session.RefreshToken = _refreshTokenGenerator.GenerateRefreshToken();
            session.Expires = DateTime.Now.AddDays(1);

            await _dbContext.SaveChangesAsync();

            var dto = new RefreshSessionCommandDto()
            {
                Jwt = jwt,
                RefreshToken = session.RefreshToken,
                Expires = session.Expires,
            };

            return dto;
        }
    }
}
