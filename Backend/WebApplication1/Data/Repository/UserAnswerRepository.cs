using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Abstractions;
using WebApplication1.Data.Entitiy;
using WebApplication1.DTOs;

namespace WebApplication1.Data.Repository
{
    public class UserAnswerRepository : IUserAnswerRepository
    {
        private readonly ApiDbContext _context;

        public UserAnswerRepository(ApiDbContext apiDbContext)
        {
            _context = apiDbContext;

        }

        public async Task<int> CorrectAnswersCount(List<UserTestDoneAnswerRequest> userAnswers)
        {
            int CorrectAnswersCount = 0;

            //вытаскиваю уникальные QuestionId из userAnswers
            var QuastionIds = userAnswers
                .Select(x => x.QuestionId)
                .Distinct()
                .ToList();
            //Формирую список QuestonEntity по QuestionId
            var Questions = await _context.Questions
                .AsNoTracking()
                .Where(x => QuastionIds.Contains(x.Id))
                .ToListAsync();

            //Для каждого QuestionEntity
            foreach(var question in Questions)
            {
                //Все ответы правильные
                bool CorrectAnswer = true;

                //Id всех корректных (IsCorrect == true)
                //QuestionOption для текущего QuestionEntity
                var QuestionCorrectOptions = await _context.QuestionOptions
                    .AsNoTracking()
                    .Where(x => x.QuestionId == question.Id && x.IsCorrect == true)
                    .Select(x => x.Id)
                    .ToListAsync();
                    
                //userAnswers сортируются по принадлежности к текущему QuestionEntity
                //и вытаскиваются QuestionptionId
                //(ответы которые дал пользователь на QuestionEntity)
                var userAnswerOptionIds = userAnswers
                                            .Where(x => x.QuestionId == question.Id)
                                            .Select(x => x.QuestionOptionId)
                                            .ToList();

                //Если количество правильных ответов и
                //количество ответов пользователя не совпадают
                if (QuestionCorrectOptions.Count != userAnswerOptionIds.Count)
                    CorrectAnswer = false;
                
                //каждый ли ответ пользователя содержится в списке правильных ответов
                foreach(var optionId in userAnswerOptionIds)
                {
                    //если в списке правильных ответов не оказалось ответа пользователя
                    if (!QuestionCorrectOptions.Contains(optionId)) 
                            CorrectAnswer = false;
                }

                if (CorrectAnswer) CorrectAnswersCount++;
            }

            return CorrectAnswersCount;
        }

        public async Task CreateRange(List<UserTestDoneAnswerRequest> userAnswers, Guid testResultId)
        {
            var userAnswersToCreate = userAnswers.Select(x => new UserAnswerEntity
            {
                Id = Guid.NewGuid(),
                TestResultId = testResultId,
                QuestionId = x.QuestionId,
                QuestionOptionId = x.QuestionOptionId,
                IsCorrect = _context.QuestionOptions.Find(x.QuestionOptionId).IsCorrect
            }).ToList();

            await _context.UserAnswers
                .AddRangeAsync(userAnswersToCreate);

            await _context.SaveChangesAsync();
        }

    }
}
