

namespace Front.DTOs
{

    public class TestResultResponse
    {
        //TODO: ВОЗМОЖНООШИБКА Я ДОБАВИЛ СТРОКУ
        public string testTitle { get; set; }= String.Empty;
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TestId { get; set; }
        public int ResultAnswers { get; set; }
        public int ResultPercent { get; set; }
        public string ResultDateTime { get; set; }
    }
}



