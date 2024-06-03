using WebApplication1.Data.Entitiy;

namespace WebApplication1.Abstractions
{
    public interface IQuestionRepository
    {
        Task<List<QuestionEntity>> GetByTestId(Guid testId);
        public Task Create(QuestionEntity entity);
        public Task<QuestionCorrectInfoEntity> GetQuestionCorrectInfoByQuestionId(Guid questionId);
    }
}