using WebApplication1.Abstractions;

namespace WebApplication1.Services
{
    public class CustomFilterService : ICustomFilterService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITestRepository _testRepository;
        public CustomFilterService(IUserRepository userRepository, ITestRepository testRepository)
        {
            _userRepository = userRepository;
            _testRepository = testRepository;
        }

        public async Task<bool> IsCompanySimilars(Guid asckerId, Guid userId)
        {
            return await _userRepository.IsCompaniesSimilar(asckerId, userId);
        }

        public async Task<string> GetUserCompanyName(Guid id)
        {
            return await _userRepository.GetUserCompany(id);
        }
        public async Task<string> GetTestCompanyOwner(Guid testId)
        {
            var test = await _testRepository.GetById(testId);
            return test.CompanyOwners;
        }

        public async Task<bool> IsEmailRegistered(string email)
        {
            var user = await _userRepository.FindByEmail(email);
            return user != null;
        }

        public async Task<int?> GetUserStatusByEmail(string email)
        {
            var user = await _userRepository.FindByEmail(email);
            return user.Status;
        }
    }
}
