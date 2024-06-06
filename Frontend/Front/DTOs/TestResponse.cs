namespace Front.DTOs
{
    public record TestResponse(
        Guid Id,
        string Title,
        string Description,
        string Category,
        int Level,
        DateTimeOffset CreatedDate,
        bool IsPrivate,
        string CompanyOwner);

}
