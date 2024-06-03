using AutoMapper;
using WebApplication1.Abstractions;
using WebApplication1.Data.Entitiy;
using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    public class AdminService : IAdminService
    {
        private readonly ITestRepository _testRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionOptionRepository _questionOptionRepository;
        private readonly IQuestionsCorrectInfoRepository _questionsCorrectInfoRepository;
        private readonly IMapper _mapper;

        public AdminService(ITestRepository testRepository,
                            IUserRepository userRepository,
                            IQuestionRepository questionRepository,
                            IQuestionOptionRepository questionOptionRepository,
                            IQuestionsCorrectInfoRepository questionsCorrectInfoRepository,
                            IMapper mapper)
        {
            _testRepository = testRepository;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
            _questionOptionRepository = questionOptionRepository;
            _questionsCorrectInfoRepository = questionsCorrectInfoRepository;
            _mapper = mapper;
        }

        public async Task<List<TestResponse>> GetCompanyTests(Guid adminId)
        {
            var testList = await _testRepository.GetCompanyTests(adminId);

            return testList.Select(x => new TestResponse (x.Id,
                                                          x.Title,
                                                          x.Description,
                                                          x.Category,
                                                          x.Level,
                                                          x.CreatedDate.UtcDateTime,
                                                          x.IsPrivate,
                                                          x.CompanyOwners)).ToList();
        }

        public async Task<bool> DeleteTestOfCompany(Guid testId, Guid adminId)
            => await _testRepository.DeleteTestOfCompany(testId, adminId);

        public async Task<bool> GetStatusToStaff(Guid userId, Guid adminId, int status)
            => await _userRepository.GetStatusToStaff(userId, adminId, status);

        public async Task<bool> StaffDelete(Guid userId, Guid adminId)
            => await _userRepository.DeleteStaff(userId, adminId);

        public async Task<bool> CreateTest (Guid adminId, CreateTestRequest createTestRequest)
        {
            bool isSuccsess = true;

            var companyName = await _userRepository.GetUserCompany(adminId);
            var testEntity = _mapper.Map<TestEntity>(createTestRequest);
            testEntity.CompanyOwners = companyName;
            await _testRepository.Create(testEntity, createTestRequest);

            return isSuccsess;
        }
    }
}
