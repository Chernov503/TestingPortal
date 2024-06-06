namespace Front.DTOs
{
    public record UserTestDoneAnswerRequest(
        Guid TestId,
        Guid? TestResultId,
        Guid QuestionId,
        Guid QuestionOptionId);
}


