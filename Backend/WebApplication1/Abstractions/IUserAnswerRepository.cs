using WebApplication1.Data.Entitiy;
using WebApplication1.DTOs;

namespace WebApplication1.Abstractions
{
    public interface IUserAnswerRepository
    {
        Task<int> CorrectAnswersCount(List<UserTestDoneAnswerRequest> userAnswers);
        public Task CreateRange(List<UserTestDoneAnswerRequest> userAnswers, Guid testResultId);
    }
}