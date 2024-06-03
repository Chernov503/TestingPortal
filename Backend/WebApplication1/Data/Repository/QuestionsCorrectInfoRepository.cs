using Microsoft.EntityFrameworkCore;
using WebApplication1.Abstractions;
using WebApplication1.Data.Entitiy;

namespace WebApplication1.Data.Repository
{
    public class QuestionsCorrectInfoRepository : IQuestionsCorrectInfoRepository
    {
        private readonly ApiDbContext _context;
        public QuestionsCorrectInfoRepository(ApiDbContext apiDbContext)
        {
            _context = apiDbContext;
        }

        public async Task<List<UserAnswerEntity>> GetByTestResultId(Guid testResultId)
            => await _context.UserAnswers
                .AsNoTracking()
                .Where(x => x.TestResultId == testResultId)
                .ToListAsync();

        public async Task Create(QuestionCorrectInfoEntity entity)
        {
            await _context.QuestionCorrectInfo.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
