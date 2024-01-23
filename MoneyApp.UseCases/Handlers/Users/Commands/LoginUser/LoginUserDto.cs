namespace MoneyApp.UseCases.Handlers.Users.Commands.LoginUser
{
    public class LoginUserDto
    {
        public required string JwtToken { get; set; }
        public required string RefreshToken { get; set; }
        public required DateTime RefreshTokenExpires { get; set; }
    }
}
