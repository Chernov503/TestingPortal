namespace WebApplication1.DTOs
{
    public record TestStatisticResponse(
        //<20% теста, 26 человек>
        Dictionary<int,int> MainStatisticPercentOfResultPercent,
        List<QuestionStatisticResponse> QuestionStatistics);

        
}


//var a = _context.Tests
//                        .Where(x => x.Id == testId)
//                        .Include(x => x.TestResults
//                                    .Where(tr => tr.ResultDateTime.ToUniversalTime() < endDate.ToUniversalTime()
//                                           && tr.ResultDateTime.ToUniversalTime() > startDate.ToUniversalTime()))
//                        .Include(x => x.Questions)
//                        .Include(x => x.Questions
//                                .Select(x => x.QuestionOptions))
//                        .Include(x => x.Questions
//                                .Select(x => x.QuestionOptions
//                                        .Select(x => x..UserAnswers.Count())))
//                        .Select(x => new TestStatisticResponse
//                                        (
//                                             new Dictionary<int, int>
//                                             {
//                                                 {
//                                                     20 , x.TestResults.Select(tr => tr.ResultPercent <= 20).Count()
//                                                 },
//                                                 {
//                                                     40, x.TestResults.Select(tr =>
//                                                                tr.ResultPercent <= 40
//                                                                && tr.ResultPercent > 20)
//                                                     .Count()
//                                                 },
//                                                {
//                                                     60, x.TestResults.Select(tr =>
//                                                                tr.ResultPercent <= 60
//                                                                && tr.ResultPercent > 40)
//                                                     .Count()
//                                                 },
//                                                                                                                                                   {
//                                                     80, x.TestResults.Select(tr =>
//                                                                tr.ResultPercent <= 80
//                                                                && tr.ResultPercent > 60)
//                                                     .Count()
//                                                 },
//                                                                                                                                                                                                    {
//                                                     100, x.TestResults.Select(tr =>
//                                                                tr.ResultPercent <= 100
//                                                                && tr.ResultPercent > 80)
//                                                     .Count()
//                                                 },
//                                             },
//                                             x.Questions
//                                                .Select(q => new QuestionStatisticResponse
//                                                    (
//                                                        q.QuestionTitle,
//                                                        q.UserAnswers.Where(ua => u)
//                                                    ))
//                                        ))