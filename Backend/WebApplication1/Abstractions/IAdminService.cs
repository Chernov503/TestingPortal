using WebApplication1.DTOs;

namespace WebApplication1.Abstractions
{
    public interface IAdminService
    {
        Task<bool> DeleteTestOfCompany(Guid testId, Guid adminId);
        public Task<bool> GetStatusToStaff(Guid userId, Guid adminId, int status);
        Task<bool> StaffDelete(Guid userId, Guid adminId);
        public Task<List<TestResponse>> GetCompanyTests(Guid adminId);
        public Task<bool> CreateTest(Guid adminId, CreateTestRequest createTestRequest);
    }
}