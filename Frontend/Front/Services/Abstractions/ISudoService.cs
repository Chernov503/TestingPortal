using Front.DTOs;
using Microsoft.JSInterop;

namespace Front.Services.Abstractions
{
    public interface ISudoService
    {
        public Task<List<UserResponse>> GetUsers();
        public Task<bool> DeleteUser(Guid userId);
        public Task<bool> SetAdminUser(Guid userId);
    }
}
