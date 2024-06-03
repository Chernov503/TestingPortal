using AutoMapper.Configuration.Annotations;

namespace WebApplication1.DTOs
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
