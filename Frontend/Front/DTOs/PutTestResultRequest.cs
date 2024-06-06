namespace Front.DTOs
{
    public record PutTestResultRequest(
        Guid TestId,
        List<UserTestDoneAnswerRequest> userPutAnswerRequests);
}
