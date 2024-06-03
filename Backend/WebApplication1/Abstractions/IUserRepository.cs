using WebApplication1.Data.Entitiy;

namespace WebApplication1.Abstractions
{
    public interface IUserRepository
    {
        public Task<List<UserEntity>> GetAll(Guid asckerId);
        public Task<List<UserEntity>> GetUsersHavntAccessToTest(Guid asckerId, Guid testId);
        public Task<bool> GetUserPremissions(Guid userId, int status);
        public Task<bool> DeleteUser(Guid userId);
        public Task<bool> DeleteStaff(Guid userId, Guid adminId);
        public Task<bool> GetStatusToStaff(Guid userId, Guid adminId, int status);
        public Task<string> GetUserCompany(Guid userId);
        public Task<bool> IsCompaniesSimilar(Guid asckerId, Guid userId);
        public Task Create(UserEntity entity);
        public Task<UserEntity> FindByEmail(string email);
    }
}