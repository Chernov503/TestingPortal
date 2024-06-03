using WebApplication1.Data.Entitiy;

namespace WebApplication1.Abstractions
{
    public interface IJwtProvider
    {
        string GenerateToken(UserEntity user);
    }
}