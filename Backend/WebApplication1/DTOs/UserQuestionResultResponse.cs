namespace WebApplication1.DTOs
{
    public record UserQuestionResultResponse(
            Guid Id,
            string QuestionTitle,
            List<UserOptionsResultResponse> UserOptionsResults,
            QuestionCorrectInfoResponse QuestionCorrectInfos);
}