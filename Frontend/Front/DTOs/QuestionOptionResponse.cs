
using Front.DTOs;

namespace Front.DTOs
{
    public record QuestionOptionResponse(
            Guid Id,
            Guid QuestionId,
            string Text,
            bool IsCorrect);

        
}
