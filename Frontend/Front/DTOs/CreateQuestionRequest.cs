namespace Front.DTOs
{
    public record CreateQuestionRequest(
        string QuestionTitle,
        int OptionCount,
        int CorrectOptionCount,
        CreateQuestionCorrectInfoRequest QuestionCorrectInfo,
        List<CreateQuestionOptionRequest> CreateQuestionOptions);
}
