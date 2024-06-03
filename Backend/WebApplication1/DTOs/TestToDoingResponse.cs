namespace WebApplication1.DTOs
{
    public record TestToDoingResponse(
        Guid Id,
        List<QuestionResponse> Questions);
}
