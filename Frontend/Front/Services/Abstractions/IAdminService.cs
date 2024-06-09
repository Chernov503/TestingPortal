using Front.DTOs;

namespace Front.Services.Abstractions
{
    public interface IAdminService
    {
        public Task<List<TestResponse>> GetTests();
        public Task<bool> DeleteTest(Guid testId);
        public Task<bool> CreateTest(CreateTest requestBody);
        public Task<List<UserResponse>> GetUsers();
        public Task<bool> PutUserStatus(ChangeStaffStatus changeStaff);
        public Task<bool> DeleteUser(Guid userId);
        public Task<HttpResponseMessage> SendRequest<K>(K requestBody,
                                                      HttpMethod httpMethod,
                                                      string uri);
    }
}
