namespace WebApplication1.DTOs
{
    public record PutTestResultRequest(
        Guid TestId,
        List<UserTestDoneAnswerRequest> userPutAnswerRequests);
}
