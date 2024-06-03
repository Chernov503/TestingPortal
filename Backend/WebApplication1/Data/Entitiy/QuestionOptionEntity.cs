namespace WebApplication1.Data.Entitiy
{
    public class QuestionOptionEntity
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public QuestionEntity? Question { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; } = false;
        public List<UserAnswerEntity> UserAnswers { get; set; } = new List<UserAnswerEntity>();
    }
}
