using Microsoft.AspNetCore.Mvc;
using WebApplication1.Abstractions;

namespace WebApplication1.Services
{
    public class SuperAdminService : ISuperAdminService
    {
        private readonly IUserRepository _userRepository;
        public SuperAdminService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> GetUserPremissions(Guid userId, int status)
            => await _userRepository.GetUserPremissions(userId, status);
        public async Task<bool> DeleteUser(Guid userId)
            => await _userRepository.DeleteUser(userId);
    }
}
