using Microsoft.AspNetCore.Mvc;
using PureCosmetics.AuthService.Application.Models.Requests.Role;
using PureCosmetics.AuthService.Application.ServiceContracts;
using PureCosmetics.Commons.Constants;

namespace PureCosmetics.AuthService.Api.Controllers
{
    /// <summary>
    /// Role Controller for managing roles.
    /// User created: QuanTM
    /// Created date: 2025/12/19
    /// Last modified date: 2025/12/19
    /// </summary>
    [Route(Constant.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class RoleController : ControllerBase
    {
        #region Fields and Constructor

        /// <summary>
        /// Interface for role service operations.
        /// </summary>
        private readonly IRoleService _roleService;

        /// <summary>
        /// Constructor to initialize RoleController with IRoleService.
        /// </summary>
        /// <param name="roleService"></param>
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        #endregion

        #region Writes

        /// <summary>
        /// Creates a new role.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleCreateRequest request)
        {
            var result = await _roleService.CreateRole((dynamic)request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing role.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] RoleCreateRequest request)
        {
            var result = await _roleService.CreateRole((dynamic)request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Deletes a role.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteRole([FromBody] RoleRemoveRequest request)
        {
            var result = await _roleService.DeleteRole(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        #endregion

        #region Reads

        /// <summary>
        /// Gets all roles based on the provided request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRoles([FromQuery] RoleGetsRequest request)
        {
            var result = await _roleService.GetRoles(request);
            if (!result.Any())
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Gets a role by its ID.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetRoleById([FromQuery] RoleGetRequest request)
        {
            var result = await _roleService.GetRoleById(request);
            if (result is null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        #endregion
    }
}
