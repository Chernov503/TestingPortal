namespace Front.DTOs
{
    public record CreateQuestionCorrectInfoRequest(
        string Title,
        string ImageLink,
        string VideoLink);
}
