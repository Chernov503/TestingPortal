using Microsoft.EntityFrameworkCore;
using WebApplication1.Abstractions;
using WebApplication1.Data.Entitiy;

namespace WebApplication1.Data.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApiDbContext _context;
        public QuestionRepository(ApiDbContext apiDbContext)
        {
            _context = apiDbContext;
        }

        public async Task<List<QuestionEntity>> GetByTestId(Guid testId)
            => await _context.Questions
                    .AsNoTracking()
                    .Where(x => x.TestId == testId)
                    .ToListAsync();

        public async Task<QuestionCorrectInfoEntity> GetQuestionCorrectInfoByQuestionId(Guid questionId)
            => await _context.QuestionCorrectInfo.SingleOrDefaultAsync(x => x.QuestionId == questionId);

        public async Task Create(QuestionEntity entity)
        {
            await _context.Questions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }

}
