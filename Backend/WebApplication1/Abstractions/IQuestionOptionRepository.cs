using WebApplication1.Data.Entitiy;

namespace WebApplication1.Abstractions
{
    public interface IQuestionOptionRepository
    {
        Task<List<QuestionOptionEntity>> GetByQuestionId(Guid questionId);
        public Task Create(QuestionOptionEntity questionOptionEntity);
    }
}