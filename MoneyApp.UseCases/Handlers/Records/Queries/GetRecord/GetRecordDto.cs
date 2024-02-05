namespace MoneyApp.UseCases.Handlers.Records.Queries.GetRecord
{
    public class GetRecordDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public decimal Change { get; set; }
        public DateTime Created { get; set; }
    }
}