using Microsoft.EntityFrameworkCore;
using WebApplication1.Abstractions;
using WebApplication1.Data.Entitiy;
using WebApplication1.DTOs;

namespace WebApplication1.Data.Repository
{
    public class TestResultRepository : ITestResultRepository
    {
        private readonly ApiDbContext _context;
        public TestResultRepository(ApiDbContext apiDbContext)
        {
            _context = apiDbContext;
        }

        public async Task<Guid> Create(
            Guid userId,
            Guid testId,
            int CorrecrAnswerCount,
            int QuestionCount)
        {

            var entity = new TestResultEntity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TestId = testId,
                ResultAnswers = CorrecrAnswerCount,
                ResultPercent = (int)((double)CorrecrAnswerCount / (double)QuestionCount * 100.0),
                ResultDateTime = DateTimeOffset.UtcNow
            };
            await _context.TestResults.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<TestResultEntity?> GetById(Guid id)
            => await _context.TestResults
                        .AsNoTracking()
                        .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<List<UserQuestionResultResponse>> GetUserOptionsResultResponsesByTestResultId(Guid? askerId,Guid testResultId, Guid userId)
        {
            if(askerId != userId)
            {
                var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Id == userId);
                var asker = await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Id == askerId);

            }

            if (!await _context.TestResults.AsNoTracking().AnyAsync(x => x.Id == testResultId && x.UserId == userId))
                throw new Exception("User Has not this testResult or access denied");

            var testEntity = await _context.Tests.SingleOrDefaultAsync(x => x.TestResults.Any(tr => tr.Id == testResultId));

            var questionResultResponseList = new List<UserQuestionResultResponse>();

            foreach (var question in await _context.Questions
                                            .Where(x => x.TestId == testEntity.Id)
                                            .ToListAsync())
            {
                var questionCorrectInfoEntity = await _context.QuestionCorrectInfo.SingleOrDefaultAsync(x => x.QuestionId == question.Id);

                var questionCorrectInfoResponse = new QuestionCorrectInfoResponse(questionCorrectInfoEntity.Id,
                                                                                  questionCorrectInfoEntity.QuestionId,
                                                                                  questionCorrectInfoEntity.Title,
                                                                                  questionCorrectInfoEntity.ImageLink,
                                                                                  questionCorrectInfoEntity.VideoLink);

                var questionResponse = new UserQuestionResultResponse(question.Id,
                                                                      question.QuestionTitle,
                                                                      new List<UserOptionsResultResponse>(),
                                                                      questionCorrectInfoResponse);

                foreach(var option in await _context.QuestionOptions.Where(x => x.QuestionId == question.Id).ToListAsync())
                {
                    var isChosen = await _context.UserAnswers.AnyAsync(x => x.TestResultId == testResultId
                                                            && x.QuestionOptionId == option.Id);

                    var userOptionsResultResponse = new UserOptionsResultResponse(option.Text, option.IsCorrect, isChosen);

                    questionResponse.UserOptionsResults.Add(userOptionsResultResponse);
                }

                questionResultResponseList.Add(questionResponse);
            }

            return questionResultResponseList;

            //return await _context.UserAnswers
            //        .AsNoTracking()
            //        .Where(x => x.TestResultId == testResultId)
            //        .Include(x => x.Question)
            //        .ThenInclude(x => x.QuestionOptions)
            //        .Include(x => x.Question.QuestionCorrectInfo)
            //        .Select(x => new UserQuestionResultResponse
            //        (
            //                x.Question.Id,

            //                x.Question.QuestionTitle,

            //                x.Question.QuestionOptions
            //                            .Select(qo => new UserOptionsResultResponse(
            //                                qo.Text,
            //                                qo.IsCorrect,
            //                                qo.Id == x.QuestionOptionId)).ToList(),

            //                                new QuestionCorrectInfoResponse(
            //                                 x.Question.QuestionCorrectInfo.Id,
            //                                 x.Question.Id,
            //                                x.Question.QuestionCorrectInfo.Title,
            //                                x.Question.QuestionCorrectInfo.ImageLink,
            //                                x.Question.QuestionCorrectInfo.VideoLink)))
            //        .ToListAsync();

        }

        public async Task<List<TestResultEntity>> GetUserResults(Guid id,
                                                                 Guid askerId,
                                                                 DateTimeOffset startDate,
                                                                 DateTimeOffset endDate)
            => await _context.TestResults
                        .AsNoTracking()
                        .Where(x => x.UserId == id
                                && x.ResultDateTime >= startDate
                                && x.ResultDateTime <= endDate)
                        .ToListAsync();

        public async Task<bool> DeleteUserTestResult(Guid id, Guid staffId, Guid resultId)
        {
            var testResult = await _context.TestResults.Where(x => x.Id == resultId).ExecuteDeleteAsync();

            return await _context.TestResults.AsNoTracking().AnyAsync(x => x.Id == resultId);
        }
    }
}
