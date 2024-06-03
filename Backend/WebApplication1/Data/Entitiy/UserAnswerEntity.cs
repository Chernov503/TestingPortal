namespace WebApplication1.Data.Entitiy
{
    public class UserAnswerEntity
    {
        public Guid Id { get; set; }
        public Guid TestResultId { get; set; }
        public TestResultEntity? TestResult { get; set; }
        public Guid QuestionId { get; set; }
        public QuestionEntity? Question { get; set; }
        public Guid QuestionOptionId { get; set; }
        public QuestionOptionEntity? QuestionOption { get; set; }
        public bool IsCorrect { get; set; }


    }
}
