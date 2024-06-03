namespace WebApplication1.DTOs
{
    public record TestFullInfo(
        Guid Id,
        List<QuestionResponse> Questions);
}
