namespace Front.DTOs
{
    public record CreateQuestionOptionRequest (
        string Text,
        bool IsCorrect);
}
