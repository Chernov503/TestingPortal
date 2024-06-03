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
       
        public UserController(IUserService userService, ICustomFilterService customFilterService)
        {
            _userService = userService;
            _customFilterService = customFilterService;

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
        [HttpGet]
        [Route("tests/test")]

        public async Task<ActionResult<TestToDoingResponse>> GetTestById([FromBody] string testId)
        {
            var asckerId = Request.Headers["asckerId"].ToString();
            return Ok(await _userService.GetTestById(Guid.Parse(asckerId), Guid.Parse(testId)));
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
        [HttpGet]
        [Route("results/result")]
        public async Task<ActionResult<TestResultResponse>> GetTestResult([FromBody]Guid resultId)
        {
            var asckerId = Request.Headers["asckerId"].ToString();
            return await _userService.GetTestResultToUser(null, Guid.Parse(asckerId), resultId);
        }

        //TODO: Пользователь получает список результатов

    }
}
