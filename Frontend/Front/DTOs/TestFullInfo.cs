namespace Front.DTOs
{
    public class TestFullInfo
    {
        public Guid Id { get; set; }
        public List<QuestionResponse> Questions { get; set; } = new List<QuestionResponse>();
    }
}
