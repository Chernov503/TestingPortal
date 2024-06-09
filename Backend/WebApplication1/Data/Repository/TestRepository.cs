using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using WebApplication1.Abstractions;
using WebApplication1.Data.Entitiy;
using WebApplication1.DTOs;

namespace WebApplication1.Data.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public TestRepository(ApiDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;


        }


        private IQueryable<TestEntity> GetPublicTests()
            => _context.Tests
                    .AsNoTracking()
                    .Where(x => x.IsPrivate == false);


        public async Task<TestEntity> GetById(Guid testId) 
            => await _context.Tests
                    .AsNoTracking()
                    .SingleAsync(x => x.Id == testId);


        public async Task<List<TestEntity?>> GetTestByUserIdAndPublic(Guid Id)
        {
            var privateTests = _context.AccesToTests
                .AsNoTracking()
                .Where(x => x.UserId == Id)
                .Include(x => x.Test)
                .Select(x => x.Test);

            var publicTests = GetPublicTests();

            return await privateTests.Concat(publicTests).ToListAsync();
        }


        public async Task<List<TestEntity>> GetCompanyTests(Guid asckerId)
        {
            var ascker = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == asckerId)
                ?? throw new Exception("Ascker Id NotFound");

            return await _context.Tests
                        .Where(x => String.Equals(
                                    x.CompanyOwners.ToLower(), 
                                    ascker.Company.ToLower()))
                        .ToListAsync();
        }


        public async Task<List<TestEntity>> GetByCompanyNameAndPublic(Guid ModeratorId)
        {
            var moderator = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == ModeratorId)
                ?? throw new Exception("Moderator Id NotFound");

            var publicTests = GetPublicTests();

            var privateTests = _context.Tests
                .Where(x => String.Equals(x.CompanyOwners.ToLower(), moderator.Company.ToLower()));

            return await privateTests.Concat(publicTests).ToListAsync();
        }


        public async Task<bool> HasUserAccesToTest(Guid userId, Guid testId)
            => await _context.AccesToTests
                                .AsNoTracking()
                                .Select(x =>
                                    x.UserId == userId
                                    && x.TestId == testId)
                                .AnyAsync();



        public async Task<bool> HasModeratorAccesToTest(Guid userId, Guid testId)
        {
            
                var test = await _context.Tests
                                        .AsNoTracking()
                                        .Where(x => x.Id == testId)
                                        .FirstOrDefaultAsync();

                if (test == null)
                {
                    return false; // Если тест не найден, возвращаем false
                }

                var userCompany = await _context.Users
                                                .AsNoTracking()
                                                .Where(u => u.Id == userId)
                                                .Select(u => u.Company.ToLower())
                                                .FirstOrDefaultAsync();

                if (userCompany == null)
                {
                    return false; // Если компания пользователя не найдена, возвращаем false
                }

                var hasAccess = test.IsPrivate == false
                                || (test.CompanyOwners.ToLower() == userCompany)
                                && await _context.Users
                                                .AsNoTracking()
                                                .AnyAsync(u => u.Id == userId && u.Status == 1);

                return hasAccess;
            
        }
            
        public async Task<TestFullInfo> GetFullTestInfo(Guid testId)
        {
            var testEntity = _context.Tests
                .Where(x => x.Id == testId)
                .Include(x => x.Questions)
                    .ThenInclude(q => q.QuestionOptions)
                .Include(x => x.Questions)
                    .ThenInclude(q => q.QuestionCorrectInfo);

            var testFullInfo = await testEntity.Select(x => new TestFullInfo(x.Id, null)).FirstOrDefaultAsync();
            //var testFullInfo = _mapper.Map<TestFullInfo>(testEntity.SingleOrDefaultAsync());

            List<QuestionResponse> qList = new List<QuestionResponse>();



            foreach (var question in _context.Questions.Where(x => x.TestId == testFullInfo.Id).ToList())
            {
                var questionR =  new QuestionResponse(question.Id,
                                                      testFullInfo.Id,
                                                      question.QuestionTitle,
                                                      question.OptionCount,
                                                      question.CorrectOptionCount,
                                                      null,
                                                      null);

                var QuestionCorrectInfoEntity = await _context.QuestionCorrectInfo.FirstOrDefaultAsync(x => x.QuestionId == questionR.Id);

                var QuestionCorrectInfoR = new QuestionCorrectInfoResponse(QuestionCorrectInfoEntity.Id,
                                                                           question.Id,
                                                                           QuestionCorrectInfoEntity.Title,
                                                                           QuestionCorrectInfoEntity.ImageLink,
                                                                           QuestionCorrectInfoEntity.VideoLink);

                questionR = questionR with { QuestionCorrectInfo = QuestionCorrectInfoR };

                List<QuestionOptionResponse> QOList = new List<QuestionOptionResponse>();

                foreach(var QO in _context.QuestionOptions.Where(x => x.QuestionId == questionR.Id).ToList())
                {
                    var QOR = _mapper.Map<QuestionOptionResponse>(QO);
                    QOR = QOR with { IsCorrect = QO.IsCorrect };
                    QOList.Add(QOR);
                }

                questionR = questionR with { QuestionOptions = QOList };

                qList.Add(questionR);
            }

            testFullInfo = testFullInfo with { Questions = qList };

            return testFullInfo;
        }


        public async Task<bool> IsTestPublic(Guid testId)
            => await _context.Tests
                        .AsNoTracking()
                        .Where(x => x.Id == testId)
                        .Select(x => x.IsPrivate)
                        .SingleOrDefaultAsync();


        public async Task<bool> HasUserAccessToTestStatistic(Guid userId, Guid testId)
        {
            bool IsModerator = await _context.Users
                        .AsNoTracking()
                        .Where(x => x.Id == userId)
                        .AnyAsync(x => x.Status >= 1);

            bool IsCompanyOwnTest = await _context.Users
                        .AsNoTracking()
                        .AnyAsync(x => x.Id == userId
                                && String.Equals(x.Company.ToLower(), _context.Tests
                                            .AsNoTracking()
                                            .Where(t => t.Id == testId)
                                            .Select(t => t.CompanyOwners.ToLower())));

            return IsModerator && IsCompanyOwnTest;

        }


        public async Task<TestStatisticResponse> GetTestStatistic(
            Guid asckerId,
            Guid testId,
            bool isPrivate,
            DateTimeOffset startDate,
            DateTimeOffset endDate)
        {


            startDate = startDate.ToUniversalTime();
            endDate = endDate.ToUniversalTime();

            IQueryable<TestResultEntity> TestResultQueryable;

            if (isPrivate)
            {
                var ascker = await _context.Users.FindAsync(asckerId);

                TestResultQueryable = _context.TestResults.Where(x => x.TestId == testId
                                                                && x.User.Company == ascker.Company
                                                                && x.ResultDateTime >= startDate
                                                                && x.ResultDateTime <= endDate);


            }
            else
            {
                TestResultQueryable = _context.TestResults.Where(x => x.TestId == testId
                                                                && x.ResultDateTime >= startDate
                                                                && x.ResultDateTime <= endDate);
            }


            List<TestResultEntity> testResultEntities = TestResultQueryable.ToList();

            var MainStatisticPercentOfResultPercent = new Dictionary<int, int>()
            {
                {20, testResultEntities.Where(x => x.ResultPercent <= 20).Count() },
                {40, testResultEntities.Where(x => x.ResultPercent <= 40 && x.ResultPercent > 20).Count() },
                {60, testResultEntities.Where(x => x.ResultPercent <= 60 && x.ResultPercent > 40).Count() },
                {80, testResultEntities.Where(x => x.ResultPercent <= 80 && x.ResultPercent > 60).Count() },
                {100, testResultEntities.Where(x => x.ResultPercent <= 100 && x.ResultPercent > 80).Count() }
            };



            List<QuestionStatisticResponse> QuestionStatistics = await
                _context.Questions
                .Where(x => x.TestId == testId)
                .AsNoTracking()
                .Select(x => new QuestionStatisticResponse
                        (
                            x.QuestionTitle,


                        // Начало запроса
                            TestResultQueryable
                            .SelectMany(tr => tr.UserAnswers) // Получение всех UserAnswers связанных с TestResult
                            .GroupBy(ua => ua.TestResultId) // Группировка по TestResultId
                            // Фильтрация по условию, что все UserAnswer в группе должны соответствовать заданным условиям
                            .Where(group => group.All(ua => ua.QuestionId != x.Id || ua.IsCorrect))
                            // Далее подсчет количества групп сущностей UserAnswer
                            .Count(),


                            x.QuestionOptions
                                .Select(qo => new QuestionOptionsStatisticResponse
                                        (
                                                qo.Text,

                                                qo.IsCorrect,

                                                _context.UserAnswers
                                            .Where(ua => testResultEntities.Select(x => x.Id).Contains(ua.TestResultId)
                                                && ua.QuestionOptionId == qo.Id)
                                .Count()
                                        )
                                ).ToList()
                        )
                ).ToListAsync();

            var TestStatistic = new TestStatisticResponse(
                    MainStatisticPercentOfResultPercent,
                    QuestionStatistics);

            return TestStatistic;
        }


        public async Task<TestStatisticResponse> GetStaffTestStatistic(
            Guid userId,
            Guid testId,
            DateTimeOffset startDate,
            DateTimeOffset endDate
            )
        {
            var CompanyName = await _context.Users
                    .AsNoTracking()
                    .Where(x => x.Id == userId)
                    .Select(x => x.Company.ToLower())
                    .SingleOrDefaultAsync()
                    ?? throw new Exception("User not found");

            var MainStatisticPercentOfResultPercent = await
                 _context.Tests
                        .AsNoTracking()
                        .Where(x => x.Id == testId)
                        .Include(x => x.TestResults
                                    .Where(tr => tr.ResultDateTime < endDate
                                           && tr.ResultDateTime > startDate
                                           && String.Equals(tr.User.Company.ToLower(), CompanyName.ToLower())))
                        .Select(x => new Dictionary<int, int>
                                    {
                                            {
                                                20, x.TestResults.Select(tr => tr.ResultPercent <= 20).Count()
                                            },
                                            {
                                                40, x.TestResults.Select(tr =>
                                                        tr.ResultPercent <= 40
                                                        && tr.ResultPercent > 20).Count()
                                            },
                                            {
                                                60, x.TestResults.Select(tr =>
                                                        tr.ResultPercent <= 60
                                                        && tr.ResultPercent > 40).Count()
                                            },
                                            {
                                                80, x.TestResults.Select(tr =>
                                                        tr.ResultPercent <= 80
                                                        && tr.ResultPercent > 60).Count()
                                            },
                                            {
                                                100, x.TestResults.Select(tr =>
                                                        tr.ResultPercent <= 100
                                                        && tr.ResultPercent > 80).Count()
                                            }
                                    }
                        ).SingleOrDefaultAsync();

            List<QuestionStatisticResponse> QuestionStatistics = await
                _context.Questions
                .AsNoTracking()
                .Select(x => new QuestionStatisticResponse
                        (
                            x.QuestionTitle,

                            x.UserAnswers
                                .Where(ua => ua.TestResult.TestId == testId
                                    && ua.TestResult.ResultDateTime >= startDate
                                    && ua.TestResult.ResultDateTime <= endDate
                                    && String.Equals(
                                                ua.TestResult.User.Company.ToLower(),
                                                CompanyName.ToLower()))
                                .ToList()
                                .Count(),

                            x.QuestionOptions
                                .Select(qo => new QuestionOptionsStatisticResponse
                                        (
                                                qo.Text,

                                                qo.IsCorrect,

                                                qo.UserAnswers
                                                    .Where(qoua => qoua.TestResult.TestId == testId
                                                        && qoua.TestResult.ResultDateTime >= startDate
                                                        && qoua.TestResult.ResultDateTime <= endDate
                                                        && String.Equals(
                                                                qoua.TestResult.User.Company.ToLower(),
                                                                CompanyName.ToLower()))
                                                    .Select(x => x.Id)
                                                    .Count()
                                        )
                                ).ToList()
                        )
                ).ToListAsync();

            var TestStatistic = new TestStatisticResponse(
                    MainStatisticPercentOfResultPercent,
                    QuestionStatistics);

            return TestStatistic;

        }


        public async Task<bool> DeleteTestOfCompany(Guid testId, Guid adminId)
        {
            var admin = await _context.Users
                        .SingleOrDefaultAsync(x => x.Id == adminId) 
                        ?? throw new ArgumentNullException(nameof(adminId));

            var test = await _context.Tests
                        .SingleOrDefaultAsync(x => x.Id == testId)
                        ?? throw new ArgumentNullException(nameof(testId));

            if (!String.Equals(test.CompanyOwners.ToLower(), admin.Company.ToLower())) 
                    throw new FieldAccessException(nameof(test.CompanyOwners));

            await _context.Tests
                        .Where(x => x.Id == testId)
                        .ExecuteDeleteAsync();

            return !await _context.Tests.AnyAsync(x => x.Id == testId);
        }


        public async Task Create(TestEntity entity, CreateTestRequest request)
        {
            await _context.Tests.AddAsync(entity);

            foreach (var question in request.Questions)
            {
                var questionEntity = _mapper.Map<QuestionEntity>(question);
                questionEntity.TestId = entity.Id;
                await _context.Questions.AddAsync(questionEntity);

                var questionCorrectInfoEntity = _mapper.Map<QuestionCorrectInfoEntity>(question.QuestionCorrectInfo);
                questionCorrectInfoEntity.QuestionId = questionEntity.Id;
                await _context.QuestionCorrectInfo.AddAsync(questionCorrectInfoEntity);

                foreach (var questionOption in question.CreateQuestionOptions)
                {
                    var questionOptionEntity = _mapper.Map<QuestionOptionEntity>(questionOption);
                    questionOptionEntity.QuestionId = questionEntity.Id;
                    await _context.QuestionOptions.AddAsync(questionOptionEntity);
                }
            }





            await _context.SaveChangesAsync();
        }
    }

}
