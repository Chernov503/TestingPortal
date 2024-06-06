namespace Front.DTOs
{
    public record UserOptionsResultResponse(
    string Text,
    bool IsCorrect,
    bool IsChosen
    );
}