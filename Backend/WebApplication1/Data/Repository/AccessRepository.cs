using Microsoft.EntityFrameworkCore;
using WebApplication1.Abstractions;
using WebApplication1.Data.Entitiy;
using WebApplication1.DTOs;

namespace WebApplication1.Data.Repository
{
    public class AccessRepository : IAccessRepository
    {
        private readonly ApiDbContext _context;
        public AccessRepository(ApiDbContext apiDbContext)
        {
            _context = apiDbContext;
        }

        public async Task GetAccessToUsers(Guid asckerId, Guid testId, List<AccessRequest> accessList)
        {
            await _context.AccesToTests.AddRangeAsync(accessList.Select(x => new AccesToTestEntity
            {
                Id = Guid.NewGuid(),
                UserId = x.UsrId,
                TestId = x.TestId
            }));

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAccessToTest(Guid staffId, Guid testId)
        {
            await _context.AccesToTests
                .Where(x => x.UserId == staffId
                        && x.TestId == testId)
                .ExecuteDeleteAsync();

            return await _context.AccesToTests
                .AsNoTracking()
                .AnyAsync(x => x.TestId == testId
                        && x.UserId == staffId);
        }
    }
}
