namespace WebApplication1.Data.Entitiy
{
    public class TestResultEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
        public Guid TestId { get; set; }
        public TestEntity? Test { get; set; }
        public int ResultAnswers { get; set; }
        public int ResultPercent { get; set; }
        public DateTimeOffset ResultDateTime { get; set; }
        public List<UserAnswerEntity>? UserAnswers { get; set; } = new List<UserAnswerEntity>();
    }
}
