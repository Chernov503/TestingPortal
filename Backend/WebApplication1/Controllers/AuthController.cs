using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Abstractions;
using WebApplication1.DTOs;

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
        public async Task<ActionResult<string>> Login([FromBody] LoginUserRequest request)
        {
            try
            {
                var token = await _userService.Login(request);
                return Ok(token);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost]
        [Route("/register")]
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
