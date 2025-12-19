using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PureCosmetics.AuthService.Application.Models.Requests.Address;
using PureCosmetics.AuthService.Application.ServiceContracts;
using PureCosmetics.Commons.Constants;

namespace PureCosmetics.AuthService.Api.Controllers
{
    /// <summary>
    /// API Controller for managing addresses.
    /// User create: QuanTM
    /// Created date: 2025/12/12
    /// Last modified date: 2025/12/12
    /// </summary>
    [Route(Constant.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class AddressController : ControllerBase
    {
        #region Fields and Constructors
        /// <summary>
        /// Interface for address service operations.
        /// </summary>
        private readonly IAddressService _addressService;

        /// <summary>
        /// Constructor to initialize AddressController with IAddressService.
        /// </summary>
        /// <param name="addressService"></param>
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        #endregion

        #region Writes

        /// <summary>
        /// Api endpoint to create a new address.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateAddress([FromBody] AddressCreateRequest request)
        {
            var result = await _addressService.CreateAddress(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Api endpoint to update an existing address.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressUpdateRequest request)
        {
            var result = await _addressService.UpdateAddress(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Api endpoint to delete an address.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteAddress([FromBody] AddressDeleteRequest request)
        {
            var result = await _addressService.DeleteAddress(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        #endregion

        #region Reads

        /// <summary>
        /// Api endpoint to get address by id.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAddressesById([FromQuery] AddressGetByIdRequest request)
        {
            var result = await _addressService.GetAddressesById(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Api endpoint to get list of addresses without pagination and filtering.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAddresses([FromQuery] AddressGetsRequest request)
        {
            var result = await _addressService.GetAddresses(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Api endpoint to get addresses by user id.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAddressesByUserId([FromQuery] AddressGetByUserIdRequest request)
        {
            var result = await _addressService.GetAddressesByUserId(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        #endregion
    }
}
