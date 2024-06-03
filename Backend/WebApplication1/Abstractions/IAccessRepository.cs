using WebApplication1.DTOs;

namespace WebApplication1.Abstractions
{
    public interface IAccessRepository
    {
        public Task GetAccessToUsers(Guid asckerId, Guid testId, List<AccessRequest> accessList);
        public Task<bool> DeleteUserAccessToTest(Guid staffId, Guid testId);
    }
}