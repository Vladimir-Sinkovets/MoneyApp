namespace MoneyApp.UseCases.Handlers.Sessions.Commands.RefreshSession
{
    public class RefreshSessionCommandDto
    {
        public required string Jwt {  get; set; }
        public required string RefreshToken { get; set; }
        public required DateTime Expires { get; set; }
    }
}
