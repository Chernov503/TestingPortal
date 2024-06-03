namespace WebApplication1.DTOs
{
    public record QuestionResponse(
        Guid Id,
        Guid TestId,
        string QuestionTitle,
        int OptionCount,
        int CorrectOptionCount,
        List<QuestionOptionResponse> QuestionOptions,
        QuestionCorrectInfoResponse QuestionCorrectInfo);


}
