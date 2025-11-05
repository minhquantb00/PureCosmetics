using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PureCosmetics.AuthService.Application.Models.Requests.User;
using PureCosmetics.AuthService.Application.ServiceContracts;
using PureCosmetics.Commons.Constants;

namespace PureCosmetics.AuthService.Api.Controllers
{
    [Route(Constant.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest request)
        {
            var result = await _userService.CreateUser(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var result = await _userService.Login(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
