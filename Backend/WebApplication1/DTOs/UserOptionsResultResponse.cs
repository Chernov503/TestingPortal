namespace WebApplication1.DTOs
{
    public record UserOptionsResultResponse(
    string Text,
    bool IsCorrect,
    bool IsChosen
    );
}