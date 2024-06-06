namespace Front.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public string QuestionTitle { get; set; } = string.Empty;
        public int OptionCount { get; set; }
        public int CorrectOptionCount { get; set; }
    }
}
