using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Abstractions;
using WebApplication1.DTOs;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [UserAuthorise("0")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICustomFilterService _customFilterService;
        private readonly IModeratorService _moderatorService;
       
        public UserController(IUserService userService, ICustomFilterService customFilterService, IModeratorService moderatorService)
        {
            _userService = userService;
            _customFilterService = customFilterService;
            _moderatorService = moderatorService;
        }


        //Пользователь хочет получить список доступных ему тестов
        [HttpGet]
        [Route("tests")]
        public async Task<ActionResult<List<TestResponse>>> GetTests()
        {
            var asckerId = Request.Headers["asckerId"].ToString();
            return Ok(await _userService.GetTests(Guid.Parse(asckerId)));
        }




        //Пользователь хочет получить определенный тест из доступных ему
        [HttpPut]
        [Route("tests/test")]

        public async Task<ActionResult<TestToDoingResponse>> GetTestById([FromBody] Guid testId)
        {
            var asckerId = Request.Headers["asckerId"].ToString();
            return Ok(await _userService.GetTestById(Guid.Parse(asckerId), testId));
        }


        //Пользователь прошел тест, отправил реультат
        //TODO: Протестировать регистрацию результатов прохождения теста
        [HttpPost]
        [Route("tests/test")]
        public async Task<ActionResult> PostAnswersOnTest(
            [FromBody] PutTestResultRequest testResultRequest)
        {
            var asckerId = Request.Headers["asckerId"].ToString();

            await _userService.RegsterTestResult(Guid.Parse(asckerId),
                                                 testResultRequest.TestId,
                                                 testResultRequest.userPutAnswerRequests);
            return Ok();
        }


        //Пользователь получил разбор ошибок (+Таблица со справочной информацией к каждому вопросу)
        //TODO: Проверка что пользователю принадлежит этот результат
        [HttpPut]
        [Route("results/result")]
        public async Task<ActionResult<TestResultResponse>> GetTestResult([FromBody] Guid testId)
        {
            var asckerId = Request.Headers["asckerId"].ToString();
            return await _userService.GetTestResultToUser(Guid.Parse(asckerId), Guid.Parse(asckerId), testId);
        }

        //TODO: Пользователь получает список результатов

        [HttpPut]
        [Route("results")]
        public async Task<ActionResult<List<TestResultResponse>>> GetResults([FromBody] DateRange dateRange)
        {
            var asckerId = Guid.Parse(Request.Headers["asckerId"].ToString());
            var startDate = DateTimeOffset.Parse(dateRange.startDate);
            var endDate = DateTimeOffset.Parse(dateRange.endDate);

            var result = await _userService.GetResults(
                    asckerId, 
                    startDate,
                    endDate);

            return result;
        }
    }
}
