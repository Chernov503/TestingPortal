using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using WebApplication1.Abstractions;
using WebApplication1.Data.Entitiy;

namespace WebApplication1.Data.Repository
{
    public class QuestionOptionRepository : IQuestionOptionRepository
    {
        private readonly ApiDbContext _context;

        public QuestionOptionRepository(ApiDbContext apiDbContext)
        {
            _context = apiDbContext;
        }

        public async Task<List<QuestionOptionEntity>> GetByQuestionId(Guid questionId)
            => await _context.QuestionOptions
                        .AsNoTracking()
                        .Where(x => x.QuestionId == questionId)
                        .ToListAsync();

        public async Task Create(QuestionOptionEntity questionOptionEntity)
        {
            await _context.QuestionOptions.AddAsync(questionOptionEntity);
            await _context.SaveChangesAsync();
        }
    }
}
