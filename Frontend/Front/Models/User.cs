namespace Front.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Password { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public int? Status { get; set; } // null - user, 0 - expert, 1 - admin
    }
}
