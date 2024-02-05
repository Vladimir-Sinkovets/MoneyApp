namespace MoneyApp.Entities.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public ICollection<Session> Sessions { get; set; }
        public ICollection<Record> Records { get; set; }
    }
}
