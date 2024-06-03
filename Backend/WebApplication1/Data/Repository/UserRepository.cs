using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Abstractions;
using WebApplication1.Data.Entitiy;

namespace WebApplication1.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiDbContext _context;
        public UserRepository(ApiDbContext apiDbContext)
        {
            _context = apiDbContext;
        }
        public async Task<List<UserEntity>> GetAll(Guid asckerId)
        {
            var ascker = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == asckerId)
                ?? throw new Exception("Invalit asckerId");

            switch (ascker.Status)
            {
                case 0:
                    {
                        throw new Exception("Access Denied");

                    }
                case 1:
                    {
                        return await _context.Users
                                        .AsNoTracking()
                                        .Where(x => 
                                                String.Equals(x.Company.ToLower() , 
                                                            ascker.Company.ToLower()) 
                                                && x.Status < 1)
                                        .ToListAsync();
                    }
                case 2:
                    {
                        return await _context.Users
                                        .AsNoTracking() 
                                        .Where(x => String.Equals(
                                                        x.Company.ToLower(),
                                                        ascker.Company.ToLower())  
                                                && x.Status < 2)
                                        .ToListAsync();
                    }
                case 3:
                    {
                        return await _context.Users
                                        .AsNoTracking()
                                        .Where(x => x.Status < 3)
                                        .ToListAsync();
                    }
                default:
                    {
                        throw new Exception("Invalid UserStatus");
                    }
            }

        }

        public async Task<List<UserEntity>> GetUsersHavntAccessToTest(Guid asckerId, Guid testId)
        {
            var ascker = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id.Equals(asckerId));

            return await _context.Users
                .AsNoTracking()
                .Where(x => String.Equals(
                            x.Company.ToLower(),
                            ascker.Company.ToLower()))
                .Where(x => !x.AccesToTests.Any(x => x.TestId == testId))
                .ToListAsync();
        }

        public async Task<bool> GetUserPremissions(Guid userId, int status)
        {
            await _context.Users.Where(x => x.Id == userId).ExecuteUpdateAsync(p =>
                                                p.SetProperty(x => x.Status, status));

            return await _context.Users
                        .AsNoTracking()
                        .AnyAsync(x => x.Id == userId && x.Status == status);
        }
        public async Task<bool> DeleteUser(Guid userId)
        {
            await _context.Users
                .Where(x => x.Id == userId)
                .ExecuteDeleteAsync();

            return !await _context.Users
                .AsNoTracking()
                .AnyAsync(x => x.Id == userId);
        }

        public async Task<bool> DeleteStaff(Guid userId, Guid adminId)
        {
            var admin = await _context.Users
            .SingleOrDefaultAsync(x => x.Id == adminId)
            ?? throw new ArgumentNullException(nameof(adminId));

            var user = await _context.Users
                        .SingleOrDefaultAsync(x => x.Id == userId)
                        ?? throw new ArgumentNullException(nameof(userId));

            if (!String.Equals(user.Company.ToLower(), admin.Company.ToLower()))
                throw new Exception("Access denied");

            await _context.Users.Where(x => x.Id == userId).ExecuteDeleteAsync();

            return !_context.Users.Any(x => x.Id == userId);
        }
        public async Task<bool> GetStatusToStaff(Guid userId, Guid adminId, int status)
        {
            var admin = await _context.Users
                        .SingleOrDefaultAsync(x => x.Id == adminId)
                        ?? throw new ArgumentNullException(nameof(adminId));

            var user = await _context.Users
                        .SingleOrDefaultAsync(x => x.Id == userId)
                        ?? throw new ArgumentNullException(nameof(userId));

            if (!String.Equals(
                        user.Company.ToLower(), 
                        admin.Company.ToLower()))
                throw new Exception("Access denied");

            return await GetUserPremissions(userId, status);
        }

        public async Task<string> GetUserCompany(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId)
                ?? throw new Exception("Id not Found");

            return user.Company;
        }


        public async Task<bool> IsCompaniesSimilar(Guid asckerId, Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            var ascker = await _context.Users.FindAsync(asckerId);

            return String.Equals(user.Company, ascker.Company);
        }

        public async Task Create(UserEntity entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<UserEntity> FindByEmail(string email) =>
            await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

    }
}
