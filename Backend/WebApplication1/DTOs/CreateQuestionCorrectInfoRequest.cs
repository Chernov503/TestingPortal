namespace WebApplication1.DTOs
{
    public record CreateQuestionCorrectInfoRequest(
        string Title,
        string ImageLink,
        string VideoLink);
}
