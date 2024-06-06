

namespace Front.DTOs
{

    public record TestResultResponse
    {
        public Guid? Id { get; init; }
        public Guid? UserId { get; init; }
        public Guid? TestId { get; init; }
        public int? ResultAnswers { get; init; }
        public int? ResultPercent { get; init; }
        public DateTimeOffset? ResultDateTime { get; init; }
    }
}