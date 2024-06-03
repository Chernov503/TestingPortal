namespace WebApplication1.Data.Entitiy
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string? Password { get; set; }
        public string FirstName { get; set; } = String.Empty;
        public string Surname { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Company { get; set; } = String.Empty;
        public int? Status { get; set; } // null - user, 0 - expert, 1 - admin
        public List<TestResultEntity> TestResults { get; set; } = new List<TestResultEntity>();
        public List<AccesToTestEntity> AccesToTests { get; set; } = new List<AccesToTestEntity>();
    }
}
