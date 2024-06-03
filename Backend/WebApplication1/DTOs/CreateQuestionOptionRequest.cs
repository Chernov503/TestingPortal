namespace WebApplication1.DTOs
{
    public record CreateQuestionOptionRequest (
        string Text,
        bool IsCorrect);
}
