using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Abstractions;
using WebApplication1.Data.Entitiy;
using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    public class UserService : IUserService
    {

        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionOptionRepository _questionOptionRepository;
        private readonly IUserAnswerRepository _userAnswerRepository;
        private readonly ITestResultRepository _testResultRepository;
        private readonly IAccessRepository _accessRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public UserService(
            ITestRepository testRepository, 
            IMapper mapper,
            IQuestionRepository questionRepository,
            IQuestionOptionRepository questionOptionRepository,
            IUserAnswerRepository userAnswerRepository,
            ITestResultRepository testResultRepository,
            IAccessRepository accessRepository,
            IPasswordHasher passwordHasher,
            IUserRepository userRepository,
            IJwtProvider jwtProvider)
        {
            _testRepository = testRepository;
            _mapper = mapper;
            _questionRepository = questionRepository;
            _questionOptionRepository = questionOptionRepository;
            _userAnswerRepository = userAnswerRepository;
            _testResultRepository = testResultRepository;
            _accessRepository = accessRepository;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<List<TestResponse>> GetTests(Guid userId)
        {
            var testEntity = await _testRepository.GetTestByUserIdAndPublic(userId);

            var response = testEntity
                .Select(x => new TestResponse(
                    x.Id,
                    x.Title,
                    x.Description,
                    x.Category,
                    x.Level,
                    x.CreatedDate.UtcDateTime,
                    x.IsPrivate,
                    String.Empty
                   ))
                .ToList();

            return response;
        }
        

        public async Task<TestToDoingResponse?> GetTestById(Guid userId, Guid testId)
        {
            bool HasUserAcces = await _testRepository.HasUserAccesToTest(userId, testId);

            if (HasUserAcces)
            {
                var test = _mapper.Map<TestToDoingResponse>(await
                    _testRepository.GetById(testId));

                var questionList = await _questionRepository.GetByTestId(test.Id);

                var questionResponseList = questionList.Select(x => _mapper.Map<QuestionResponse>(x)).ToList();

                foreach (var quest in questionResponseList) 
                {
                    var questonResponse = quest;

                    //var questionCorrectInfoEntity = await _questionRepository.GetQuestionCorrectInfoByQuestionId(quest.Id);
                    //var questionCorrectInfoResponce = _mapper.Map<QuestionCorrectInfoResponse>(questionCorrectInfoEntity);
                    //questonResponse = questonResponse with { QuestionCorrectInfo = questionCorrectInfoResponce };

                    var questionOptionEntityList = await _questionOptionRepository.GetByQuestionId(quest.Id);
                    foreach(var questionOptionEntity in questionOptionEntityList)
                    {
                        var questionOptionResponse = _mapper.Map<QuestionOptionResponse>(questionOptionEntity);
                        questionOptionResponse = questionOptionResponse with { IsCorrect = null };
                        questonResponse.QuestionOptions.Add(questionOptionResponse);
                    }

                    test.Questions.Add(questonResponse);
                    
                }

                return test;
            }


            return null;
        }

        public async Task RegsterTestResult(
            Guid userId, 
            Guid testId, 
            List<UserTestDoneAnswerRequest> userTestDoneAnswerRequests)
        {
            var correctAnswerCount = await _userAnswerRepository.CorrectAnswersCount(userTestDoneAnswerRequests);
            
            var resultId = await _testResultRepository.Create(
                userId, 
                testId, 
                correctAnswerCount, 
                userTestDoneAnswerRequests
                    .Select(x => x.QuestionId).Distinct().Count());

            if (!resultId.Equals(Guid.Empty)) await _accessRepository.DeleteUserAccessToTest(userId, testId);
            
            await _userAnswerRepository.CreateRange(userTestDoneAnswerRequests, resultId);
        }

        public async Task<TestResultResponse> GetTestResultToUser(Guid? askerId, Guid userId, Guid testResultId)
        {

            var testResult = _mapper
                .Map<TestResultResponse>( await 
                _testResultRepository.GetById(testResultId) );

            var userQuestionsResult = await _testResultRepository.GetUserOptionsResultResponsesByTestResultId(askerId, testResultId, userId);

            testResult.UserQuestionResult.AddRange(userQuestionsResult.ToList());

            return testResult;
        }


        public async Task Register(RegisterUserRequest request)
        {
            var entity = new UserEntity
            {
                Id = Guid.NewGuid(),
                Password = _passwordHasher.Generate(request.password),
                FirstName = request.firstName,
                Surname = request.surName,
                Email = request.email,
                Company = request.company,
                Status = 0
            };

            await _userRepository.Create(entity);
        }

        public async Task<string> Login(LoginUserRequest request)
        {
            var entity = await _userRepository.FindByEmail(request.email);

            var isVerified = _passwordHasher.Verify(request.password, entity.Password);


            if (isVerified)
            {
                return _jwtProvider.GenerateToken(entity);
            }
            else throw new Exception("Bad Login");
        }

    }
}
