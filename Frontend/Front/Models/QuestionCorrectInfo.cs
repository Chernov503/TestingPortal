namespace Front.Models
{
    public class QuestionCorrectInfo
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageLink { get; set; } = string.Empty;
        public string VideoLink { get; set; } = string.Empty;
    }
}
