using WebApplication1.Data.Entitiy;
using WebApplication1.DTOs;

namespace WebApplication1.Abstractions
{
    public interface ITestResultRepository
    {
        public  Task<Guid> Create(
                                Guid userId,
                                Guid testId,
                                int CorrecrAnswerCount,
                                int QuestionCount);
        public Task<TestResultEntity?> GetById(Guid id);
        public Task<List<UserQuestionResultResponse>> GetUserOptionsResultResponsesByTestResultId(Guid askerId, Guid testResultId, Guid userId);
        public Task<List<TestResultResponse>> GetUserResults(Guid id, Guid askerId, DateTimeOffset startDate, DateTimeOffset endDate);
        public Task<bool> DeleteUserTestResult(Guid id, Guid staffId, Guid resultId);
        public Task<TestResultEntity> GetLastUserResult(Guid userId);
    }
}