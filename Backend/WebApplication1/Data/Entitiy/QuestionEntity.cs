namespace WebApplication1.Data.Entitiy
{
    public class QuestionEntity
    {
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public TestEntity? Test { get; set; }
        public string QuestionTitle { get; set; } = String.Empty;
        public int OptionCount { get; set; }
        public int CorrectOptionCount { get; set; }

        public List<QuestionOptionEntity> QuestionOptions { get; set; } = new List<QuestionOptionEntity>();
        public List<UserAnswerEntity> UserAnswers { get; set; } = new List<UserAnswerEntity>();
        public QuestionCorrectInfoEntity QuestionCorrectInfo { get; set; } = new QuestionCorrectInfoEntity();
    }
}
