namespace Front.Models
{
    public class Test
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Level { get; set; } = 0;
        public DateTimeOffset CreatedDate { get; set; }
        public bool IsPrivate { get; set; } = false;
        public string CompanyOwners { get; set; } = string.Empty;
    }
}
