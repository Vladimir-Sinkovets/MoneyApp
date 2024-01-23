using MediatR;
using MoneyApp.Entities.Models;
using MoneyApp.Infrastructure.Interfaces.DataAccess;
using MoneyApp.Infrastructure.Interfaces.Services;
using MoneyApp.UseCases.Exceptions;

namespace MoneyApp.UseCases.Handlers.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public LoginUserCommandHandler(
            IDbContext dbContext,
            IRefreshTokenGenerator refreshTokenGenerator,
            IJwtTokenGenerator tokenGenerator)
        {
            _dbContext = dbContext;
            _refreshTokenGenerator = refreshTokenGenerator;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<LoginUserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = _dbContext.Users
                .FirstOrDefault(user =>
                    user.UserName == request.UserName &&
                    user.Password == request.Password);

            if (user == null)
                throw new UserNotFoundException();
                
            var session = await CreateSessionAsync(request, user);

            var token = new LoginUserDto()
            {
                JwtToken = _tokenGenerator.Generate(user),
                RefreshToken = session.RefreshToken,
                RefreshTokenExpires = session.Expires,
            };

            return token;
        }
        private async Task<Session> CreateSessionAsync(LoginUserCommand request, User user)
        {
            var session = new Session()
            {
                Id = Guid.NewGuid(),
                App = request.App,
                RefreshToken = _refreshTokenGenerator.GenerateRefreshToken(),
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(1),
                User = user,
            };

            _dbContext.Sessions.Add(session);

            await _dbContext.SaveChangesAsync();

            return session;
        }
    }
}
