namespace MoneyApp.Entities.Models
{
    public class Session
    {
        public Guid Id { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public string App { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
