using MediatR;
using MoneyApp.Entities.Models;
using MoneyApp.Infrastructure.Interfaces.DataAccess;
using MoneyApp.Infrastructure.Interfaces.Services;
using MoneyApp.UseCases.Exceptions;

namespace MoneyApp.UseCases.Handlers.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;

        public RegisterUserCommandHandler(IDbContext dbContext,
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator)
        {
            _dbContext = dbContext;
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
        }

        public async Task<RegisterUserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = await CreateUserAsync(request);

            var session = await CreateSessionAsync(request, user);

            var dto = new RegisterUserDto()
            {
                JwtToken = _jwtTokenGenerator.Generate(user),

                RefreshToken = session.RefreshToken,

                Expires = session.Expires,
            };

            return dto;
        }

        private async Task<Session> CreateSessionAsync(RegisterUserCommand request, User user)
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

        private async Task<User> CreateUserAsync(RegisterUserCommand command)
        {
            if (_dbContext.Users.Any(u => u.Email == command.Email))
                throw new UserAlreadyExistException();

            var user = new User()
            {
                Email = command.Email,
                Password = command.Password,
                Role = "default",
                UserName = command.UserName,
            };

            _dbContext.Users.Add(user);

            await _dbContext.SaveChangesAsync();

            return user;
        }
    }
}
