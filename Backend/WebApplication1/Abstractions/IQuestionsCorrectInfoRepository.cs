using WebApplication1.Data.Entitiy;

namespace WebApplication1.Abstractions
{
    public interface IQuestionsCorrectInfoRepository
    {
        public Task<List<UserAnswerEntity>> GetByTestResultId(Guid testResultId);
        public Task Create(QuestionCorrectInfoEntity entity);
    }
}