using MediatR;
namespace MoneyApp.UseCases.Handlers.Sessions.Commands.RefreshSession
{
    public class RefreshSessionCommand : IRequest<RefreshSessionCommandDto>
    {
        public required string RefreshToken { get; set; }
    }
}
