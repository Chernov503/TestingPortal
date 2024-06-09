using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Abstractions;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Filters;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [UserAuthorise("1")]
    public class ModeratorController : ControllerBase
    {
        private readonly IModeratorService _moredatorService;
        public ModeratorController(
            IModeratorService moderatorService
            )
        {
            _moredatorService = moderatorService;
        }

        [HttpGet]
        [Route("tests")]
        public async Task<ActionResult<List<TestResponse>>> GetTests()
        {
            var asckerId = Guid.Parse(Request.Headers["asckerId"].ToString());
            return Ok(await _moredatorService.GetTests(asckerId));
        }

        [HttpGet]
        [Route("tests/{testId:guid}")]
        public async Task<ActionResult<TestFullInfo>> GetFullTestInfo(Guid testId)
        {
            var asckerId = Guid.Parse(Request.Headers["asckerId"].ToString());
            return Ok(await _moredatorService.GetFullTestById(asckerId, testId));
        }

        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<List<UserResponse>>> GetUsers()
        {
            var asckerId = Guid.Parse(Request.Headers["asckerId"].ToString());
            return Ok(await _moredatorService.GetUsers(asckerId));
        }

        [HttpGet]
        [Route("tests/{testId:guid}/access")]
        public async Task<ActionResult<UserResponse>> GetUsersHavntAccessToTest(Guid testId)
        {
            var asckerId = Guid.Parse(Request.Headers["asckerId"].ToString());
            return Ok(await _moredatorService.GetUsersHavntAccessToTest(asckerId, testId));
        }

        [HttpPost]
        [Route("tests/{testId:guid}/access")]
        public async Task<ActionResult> PostTestAccessToUsers(Guid testId, [FromBody] List<AccessRequest> accessList)
        {
            var asckerId = Guid.Parse(Request.Headers["asckerId"].ToString());
            await _moredatorService.PostAccessUsersToTest(asckerId, testId, accessList);
            return Ok();
        }

        [HttpPut]
        [Route("tests/{testId:guid}/statistic&public={isPrivate:bool}")]
        public async Task<ActionResult<TestStatisticResponse>> GetAllTestStatistic(
            Guid testId,
            bool isPrivate,
            [FromBody] DateRange dateRange)
        {
            var asckerId = Guid.Parse(Request.Headers["asckerId"].ToString());
            var startDate = DateTimeOffset.Parse(dateRange.startDate);
            var endDate = DateTimeOffset.Parse(dateRange.endDate);

            var result = await _moredatorService.GetTestStatistic(asckerId,
                                                               testId,
                                                               isPrivate,
                                                               startDate,
                                                               endDate);

            return Ok(result);
        }

        [HttpPut]
        [Route("users/{staffId:guid}/results")]
        public async Task<ActionResult<List<TestResultResponse>>> GetUserResults(Guid staffId,
                                                                                 [FromBody] DateRange dateRange)
        {
            var asckerId = Guid.Parse(Request.Headers["asckerId"].ToString());
            var startDate = DateTimeOffset.Parse(dateRange.startDate).ToUniversalTime();
            var endDate = DateTimeOffset.Parse(dateRange.endDate).ToUniversalTime();

            return Ok(await _moredatorService.GetUserResults(asckerId, staffId, startDate, endDate));
        }

        [HttpGet]
        [Route("users/{staffId:guid}/results/{resultId:guid}")]
        public async Task<ActionResult<TestResultResponse>> GetTestResult(Guid staffId, Guid resultId)
        {
            var asckerId = Guid.Parse(Request.Headers["asckerId"].ToString());
            return Ok(await _moredatorService.GetTestResultStaffUser(asckerId, staffId, resultId));
        }


        [HttpDelete]
        [Route("users/{staffId:guid}/results/{resultId:guid}")]
        public async Task<ActionResult<bool>> DeleteUserTestResult(Guid staffId, Guid resultId)
        {
            var asckerId = Guid.Parse(Request.Headers["asckerId"].ToString());
            return Ok(await _moredatorService.DeleteUserTestResult(asckerId, staffId, resultId));
        }
    }
}
