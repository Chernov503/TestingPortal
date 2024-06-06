using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Abstractions;
using WebApplication1.DTOs;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginUserRequest request)
        {
            try
            {
                var loginResponse = await _userService.Login(request);
                if (loginResponse == null) { return BadRequest("invalid email"); }
                return Ok(loginResponse);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost]
        [Route("/register")]
        [TypeFilter(typeof(RegisterFilter))]
        public async Task<ActionResult> Register([FromBody] RegisterUserRequest request)
        {
            try
            {
                await _userService.Register(request);
                return Ok();
            }
            catch { return BadRequest("Incorrect Information"); }
        }

    }
}
