namespace WebApplication1.Data.Entitiy
{
    public class TestEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Category { get; set; } = String.Empty;
        public int Level { get; set; } = 0;
        public DateTimeOffset CreatedDate { get; set; }
        public bool IsPrivate { get; set; } = false;
        public string CompanyOwners { get; set; } = String.Empty;
        public List<QuestionEntity> Questions { get; set; } = new List<QuestionEntity>();
        public List<TestResultEntity> TestResults { get; set; } = new List<TestResultEntity>();
        public List<AccesToTestEntity> AccesToTests { get; set; } = new List<AccesToTestEntity>();
    }
}
