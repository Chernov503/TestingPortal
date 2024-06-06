
namespace Front.DTOs
{
    public record CreateTestRequest(
        string Title,
        string Description,
        string Category,
        int Level,
        bool IsPrivate,
        string CompanyOwners,
        List<CreateQuestionRequest> Questions);
}
