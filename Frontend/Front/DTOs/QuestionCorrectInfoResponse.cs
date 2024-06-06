namespace Front.DTOs
{
    public record QuestionCorrectInfoResponse(
    Guid Id,
    Guid QuestionId,
    string Title,
    string ImageLink,
    string VideoLink);
}