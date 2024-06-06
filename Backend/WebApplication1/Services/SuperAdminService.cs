using Microsoft.AspNetCore.Mvc;
using WebApplication1.Abstractions;
using WebApplication1.DTOs;

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

        public async Task<List<UserResponse>> GetUsers(Guid asckerId)
        {
            var userEntityList = await _userRepository.GetAll(asckerId);

            return userEntityList.Select(x => new UserResponse(x.Id,
                                                               String.Empty,
                                                               x.FirstName,
                                                               x.Surname,
                                                               x.Email,
                                                               x.Company,
                                                               x.Status)).ToList();
        }
    }
}
