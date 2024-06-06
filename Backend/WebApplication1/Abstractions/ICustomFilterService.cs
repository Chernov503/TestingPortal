namespace WebApplication1.Abstractions
{
    public interface ICustomFilterService
    {
        Task<bool> IsCompanySimilars(Guid asckerId, Guid userId);
        public Task<string> GetUserCompanyName(Guid id);
        public Task<string> GetTestCompanyOwner(Guid testId);
        public Task<bool> IsEmailRegistered(string email);

        public Task<int?> GetUserStatusByEmail(string email);
    }
}