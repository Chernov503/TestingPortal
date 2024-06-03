namespace WebApplication1.Abstractions
{
    public interface ISuperAdminService
    {
        Task<bool> DeleteUser(Guid userId);
        Task<bool> GetUserPremissions(Guid userId, int status);
    }
}