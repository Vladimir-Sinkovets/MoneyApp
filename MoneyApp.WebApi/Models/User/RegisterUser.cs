namespace MoneyApp.WebApi.Models.User
{
    public class RegisterUser
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string App { get; set; } = string.Empty;
    }
}
