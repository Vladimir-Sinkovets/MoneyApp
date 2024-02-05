namespace MoneyApp.WebApi.Models.Record
{
    public class UpdateRecordModel
    {
        public Guid RecordId { get; set; }
        public string Text { get; set; } = string.Empty;
        public decimal Change { get; set; }
        public DateTime Date { get; set; }
    }
}
