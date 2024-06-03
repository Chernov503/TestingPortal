using WebApplication1.Data.Entitiy;
using WebApplication1.DTOs;

namespace WebApplication1.Abstractions
{
    public interface ITestRepository
    {
        public Task<TestEntity> GetById(Guid testId);
        public Task<List<TestEntity>> GetCompanyTests(Guid asckerId);
        Task<List<TestEntity?>> GetTestByUserIdAndPublic(Guid Id);
        Task<bool> HasUserAccesToTest(Guid userId, Guid testId);
        public Task<List<TestEntity>> GetByCompanyNameAndPublic(Guid ModeratorId);
        public Task<bool> HasModeratorAccesToTest(Guid userId, Guid testId);
        public Task<TestFullInfo> GetFullTestInfo(Guid testId);
        public Task<bool> IsTestPublic(Guid testId);
        public Task<TestStatisticResponse> GetTestStatistic(
            Guid asckerId,
            Guid testId,
            bool isPrivate,
            DateTimeOffset startDate,
            DateTimeOffset endDate);

        public Task<bool> HasUserAccessToTestStatistic(Guid userId, Guid testId);

        public Task<TestStatisticResponse> GetStaffTestStatistic(
            Guid userId,
            Guid testId,
            DateTimeOffset startDate,
            DateTimeOffset endDate
            );

        public Task<bool> DeleteTestOfCompany(Guid testId, Guid adminId);

        public Task Create(TestEntity entity, CreateTestRequest request);
    }
}