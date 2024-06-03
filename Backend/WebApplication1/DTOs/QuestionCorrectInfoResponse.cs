namespace WebApplication1.DTOs
{
    public record QuestionCorrectInfoResponse(
    Guid Id,
    Guid QuestionId,
    string Title,
    string ImageLink,
    string VideoLink);
}