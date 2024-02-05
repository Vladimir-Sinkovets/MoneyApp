namespace MoneyApp.WebApi.Models.Record
{
    public class AddRecordModel
    {
        public string Text { get; set; } = string.Empty;
        public decimal Change { get; set; }
        public DateTime Date { get; set; }
    }
}