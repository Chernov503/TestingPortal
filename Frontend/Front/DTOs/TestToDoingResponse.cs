namespace Front.DTOs
{
    public record TestToDoingResponse(
        Guid Id,
        List<QuestionResponse> Questions);
}
