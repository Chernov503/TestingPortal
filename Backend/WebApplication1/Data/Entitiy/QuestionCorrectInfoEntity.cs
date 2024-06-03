namespace WebApplication1.Data.Entitiy
{
    public class QuestionCorrectInfoEntity
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public QuestionEntity? Question { get; set; }
        public string Title { get; set; } = String.Empty;
        public string ImageLink { get; set; } = String.Empty;
        public string VideoLink { get; set; } = String.Empty;
    }
}
