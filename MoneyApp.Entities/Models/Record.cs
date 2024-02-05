namespace MoneyApp.Entities.Models
{
    public class Record
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public decimal Change { get; set; }
        public DateTime Created { get; set; }
    }
}
