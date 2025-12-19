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
    /// <summary>
    /// Authentication Controller for managing user authentication and authorization.
    /// User create: QuanTM
    /// Created date: 2025/12/12
    /// Last modified date: 2025/12/12
    /// </summary>
    [Route(Constant.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Fields and Constructors

        /// <summary>
        /// Interface for user service operations.
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor to initialize AuthController with IUserService.
        /// </summary>
        /// <param name="userService"></param>
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Writes

        /// <summary>
        /// Api to create a new user.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Api to update an existing user.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Api to delete an existing user.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Api for user login.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Api to get user by Id.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Api to get all users with pagination and filtering.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
