using Front.DTOs;
using Front.DTOs.TestDoing;
using Front.DTOs.TestResult;

namespace Front.Services.Abstractions
{
    public interface IUserService
    {
        public Task<List<TestResponse>> GetTests();
        public Task<ToDoTestResultRequest> GetTestById(Guid testId);
        public Task<bool> PostTestResalt(PutTestResultRequest requestBody);
        public Task<TestStatisticResponse> GetResultById(Guid resultId);
        public Task<List<TestResultResponse>> GetResults(DateRange dateRange);

    }
}
