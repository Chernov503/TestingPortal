namespace WebApplication1.Abstractions
{
    public interface IPasswordHasher
    {
        string Generate(string password);
        public bool Verify(string password, string passwordHashed);
    }
}