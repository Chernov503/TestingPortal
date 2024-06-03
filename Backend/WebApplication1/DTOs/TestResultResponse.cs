using WebApplication1.Data.Repository;

namespace WebApplication1.DTOs
{
    //public record TestResultResponse(
    //    Guid Id,
    //    Guid UserId,
    //    Guid TestId,
    //    int ResultAnswers,
    //    int ResultPercent,
    //    DateTimeOffset ResultDateTime,
    //    List<UserQuestionResultResponse> UserQuestionResult);


    public record TestResultResponse
    {
        public Guid? Id { get; init; }
        public Guid? UserId { get; init; }
        public Guid? TestId { get; init; }
        public int? ResultAnswers { get; init; }
        public int? ResultPercent { get; init; }
        public DateTimeOffset? ResultDateTime { get; init; }
        public List<UserQuestionResultResponse> UserQuestionResult { get; init; }

        public TestResultResponse()
        {
            UserQuestionResult = new List<UserQuestionResultResponse>();
        }
        public TestResultResponse(Guid id, Guid userId, Guid testId, int resultAnswers, int resultPercent, DateTimeOffset resultDateTime, List<UserQuestionResultResponse> userQuestionResult)
        {
            Id = id;
            UserId = userId;
            TestId = testId;
            ResultAnswers = resultAnswers;
            ResultPercent = resultPercent;
            ResultDateTime = resultDateTime;
            UserQuestionResult = userQuestionResult;
        }
    }
}