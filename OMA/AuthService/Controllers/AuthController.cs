using AuthServices.Models.UserModels;
using AuthServices.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using SharedService;

namespace AuthServices.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AuthController : ControllerBase
    {
        private IConfiguration _config;

        private readonly IUserService _userService;

        public AuthController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] AddUserDto addUser)
        {
            var user = _userService.Register(addUser);
            if (user != null)
                return StatusCode(201);
            return StatusCode(500);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginUserDto login)
        {

            if (_userService.CanLogin(login))
            {

                var tokenString = TokenManagerService.CreateToken(_userService.GetUserClaim(login.Email));
                return Ok(new { token = tokenString, status = true });
            }
            else
            {
                return Ok(new { token = "", status = false });

            }

        }
    }
}
