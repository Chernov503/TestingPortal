namespace Front.Models
{
    public class TestResult
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TestId { get; set; }
        public int ResultAnswers { get; set; }
        public int ResultPercent { get; set; }
        public DateTimeOffset ResultDateTime { get; set; }
    }
}
