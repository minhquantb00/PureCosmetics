using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PureCosmetics.AuthService.Application.Models.Requests.Address;
using PureCosmetics.AuthService.Application.ServiceContracts;
using PureCosmetics.Commons.Constants;

namespace PureCosmetics.AuthService.Api.Controllers
{
    [Route(Constant.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class AddressController : ControllerBase
    {
        #region Fields and Constructors
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        #endregion
        #region Writes
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
