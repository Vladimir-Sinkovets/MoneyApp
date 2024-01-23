namespace MoneyApp.UseCases.Handlers.Users.Commands.RegisterUser
{
    public class RegisterUserDto
    {
        public required string JwtToken { get; set; }
        public required string RefreshToken { get; set; }
        public required DateTime Expires { get; set; }
    }
}
