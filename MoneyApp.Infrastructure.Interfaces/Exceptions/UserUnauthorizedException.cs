namespace MoneyApp.Infrastructure.Interfaces.Exceptions
{
    public class UserUnauthorizedException : Exception
    {
        public UserUnauthorizedException() { }
        public UserUnauthorizedException(string message) : base(message) { }

        public UserUnauthorizedException(string message, Exception inner) : base(message, inner) { }
    }
}
