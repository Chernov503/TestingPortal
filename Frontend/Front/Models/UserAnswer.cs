namespace Front.Models
{
    public class UserAnswer
    {
        public Guid Id { get; set; }
        public Guid TestResultId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid QuestionOptionId { get; set; }
        public bool IsCorrect { get; set; }


    }
}
