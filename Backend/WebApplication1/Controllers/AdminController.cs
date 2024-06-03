using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Abstractions;
using WebApplication1.Data.Repository;
using WebApplication1.DTOs;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [UserAuthorise("2")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IModeratorService _moderatorService;
        public AdminController(IAdminService adminService,
                               IModeratorService moderatorService)
        {
            _adminService = adminService;
            _moderatorService = moderatorService;
        }

        [HttpGet]
        [Route("tests")]
        public async Task<ActionResult<List<UserResponse>>> GetTests()
        {
            var asckerId = Guid.Parse(HttpContext.Request.Headers["asckerId"].ToString());
            return Ok(await _adminService.GetCompanyTests(asckerId)); 
        }



        [HttpDelete]
        [Route("tests")]
        [TypeFilter(typeof(IsTestInYourCompany))]
        public async Task<ActionResult<bool>> DeleteTestOfCompany([FromBody] Guid testId)
        {
            var asckerId = Guid.Parse(HttpContext.Request.Headers["asckerId"].ToString());
            return Ok(await _adminService.DeleteTestOfCompany(testId, asckerId));
        }




        [HttpGet]
        [Route("users")]
        public async Task<ActionResult<List<UserResponse>>> GetUsers()
        {
            var asckerId = Guid.Parse(HttpContext.Request.Headers["asckerId"].ToString());
            return Ok(await _moderatorService.GetUsers(asckerId));
        }



        
        [HttpPut]
        [Route("users")]
        [TypeFilter(typeof(IsStaffInYourCompany))]

        public async Task<ActionResult<bool>> GetStatusToStaff([FromBody] ChangeStaffStatus requestBody)
        {
            var asckerId = Guid.Parse( HttpContext.Request.Headers["asckerId"].ToString() );

            if (requestBody.status > 1) return BadRequest("Invalid Staff status");

            return Ok (await _adminService.GetStatusToStaff(requestBody.userId, asckerId, requestBody.status));
        }



        [HttpDelete]
        [Route("users")]
        [TypeFilter(typeof(IsStaffInYourCompany))]
        public async Task<ActionResult<bool>> StaffDelete([FromBody] string userId)
        {
            var asckerId = Guid.Parse(HttpContext.Request.Headers["asckerId"].ToString());

            var userid = Guid.Parse(userId);

            return Ok(await _adminService.StaffDelete(userid, asckerId));
        }


        [HttpPost]
        [Route("tests/create")]
        public async Task<ActionResult<bool>> CreateTest([FromBody] CreateTestRequest request)
        {
            var asckerId = Guid.Parse(HttpContext.Request.Headers["asckerId"].ToString());

            return Ok(await _adminService.CreateTest(asckerId, request));
        }
        

    }
}
