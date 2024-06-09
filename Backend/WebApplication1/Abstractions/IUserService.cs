using WebApplication1.DTOs;

namespace WebApplication1.Abstractions
{
    public interface IUserService
    {
        public Task<List<TestResponse>> GetTests(Guid userId);
        public Task<TestToDoingResponse?> GetTestById(Guid userId, Guid testId);
        public Task RegsterTestResult(
            Guid userId,
            Guid testId,
            List<UserTestDoneAnswerRequest> userTestDoneAnswerRequests);
        public Task<TestResultResponse> GetTestResultToUser(Guid askerId, Guid userId, Guid testResultId);
        public Task<LoginResponse> Login(LoginUserRequest request);
        public Task Register(RegisterUserRequest request);
        public Task<List<TestResultResponse>> GetResults(Guid userId, DateTimeOffset startDate, DateTimeOffset endDate);
    }
    
}