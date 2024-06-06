namespace Front.DTOs
{
    public record TestFullInfo(
        Guid Id,
        List<QuestionResponse> Questions);
}
