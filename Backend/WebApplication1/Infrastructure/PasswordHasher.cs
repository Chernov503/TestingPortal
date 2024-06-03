using WebApplication1.Abstractions;

namespace WebApplication1.Infrastructure
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Generate(string password)
            => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool Verify(string password, string passwordHashed)
            => BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHashed);
    }
}
