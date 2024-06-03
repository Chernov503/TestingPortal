using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Abstractions;
using WebApplication1.DTOs;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/sudo")]
    [Authorize]
    [UserAuthorise("3")]
    public class SuperAdminController : ControllerBase
    {
        private readonly ISuperAdminService _superAdminService;
        public SuperAdminController(ISuperAdminService superAdminService)
        {
            _superAdminService = superAdminService;
        }


        [HttpPut]
        [Route("/sudo")]
        public async Task<ActionResult<bool>> GetUserPremissions([FromBody] ChangeStaffStatus request)
        {
            if (request.status > 3 || request.status < 0) return BadRequest("Incorrect user status");
            

            return Ok(await _superAdminService.GetUserPremissions(request.userId, request.status));
        }



        [HttpDelete]
        [Route("/sudo")]
        public async Task<ActionResult<bool>> DeleteUser([FromBody]string userId)
        {
            return Ok(await _superAdminService.DeleteUser(Guid.Parse(userId)));
        }




    }
}
