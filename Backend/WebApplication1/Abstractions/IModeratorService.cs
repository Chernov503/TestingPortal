using WebApplication1.DTOs;

namespace WebApplication1.Abstractions
{
    public interface IModeratorService
    {
        Task<List<TestResponse>> GetTests(Guid Id);
        public Task<TestFullInfo> GetFullTestById(Guid userId, Guid testId);
        public Task<List<UserResponse>> GetUsers(Guid asckerId);
        public Task<List<UserResponse>> GetUsersHavntAccessToTest(Guid asckerId, Guid testId);
        public Task PostAccessUsersToTest(Guid id, Guid testId, List<AccessRequest> accessList);
        public Task<TestStatisticResponse> GetTestStatistic(
            Guid asckerId,
            Guid testId,
            bool isPrivate,
            DateTimeOffset startDate,
            DateTimeOffset endDate);

        public Task<TestResultResponse> GetTestResultStaffUser(Guid askerId, Guid userId, Guid testResultId);

        public  Task<List<TestResultResponse>> GetUserResults(Guid id, Guid staffId, DateTimeOffset startDate, DateTimeOffset endDate);

        public Task<bool> DeleteUserTestResult(Guid id, Guid staffId, Guid resultId);
    }
}