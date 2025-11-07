using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PureCosmetics.AuthService.Application.Models.Requests.User;
using PureCosmetics.AuthService.Application.ServiceContracts;
using PureCosmetics.Commons.Constants;
using System.Text;

namespace PureCosmetics.AuthService.Api.Controllers
{
    [Route(Constant.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Fields and Constructors
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion
        #region Writes
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

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request)
        {
            var result = await _userService.UpdateUser(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteUser([FromBody] UserDeleteRequest request)
        {
            var result = await _userService.DeleteUser(request);
            if(!result.IsSuccess)
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
        #endregion
        #region Reads
        [HttpGet]
        public async Task<IActionResult> GetUserById([FromQuery] UserGetByIdRequest request)
        {
            var result = await _userService.GetUserById(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserGetsRequest request)
        {
            var result = await _userService.GetAllUsers(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        #endregion
    }
}
