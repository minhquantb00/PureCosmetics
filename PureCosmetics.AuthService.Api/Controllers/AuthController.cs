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

        [HttpPost]
        public IActionResult DebugJwt([FromBody] string token, [FromServices] IConfiguration cfg)
        {
            static string B64Url(byte[] x) =>
                Convert.ToBase64String(x).TrimEnd('=').Replace('+', '-').Replace('/', '_');

            var secret = (cfg["JWT:SecretKey"] ?? "").Trim();
            var parts = token?.Split('.') ?? Array.Empty<string>();
            if (parts.Length != 3) return BadRequest("Token không hợp lệ (không đủ 3 phần).");

            var signingInput = parts[0] + "." + parts[1];

            using var hmac = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(secret));
            var calcSig = B64Url(hmac.ComputeHash(Encoding.UTF8.GetBytes(signingInput)));
            var equal = string.Equals(calcSig, parts[2], StringComparison.Ordinal);

            return Ok(new
            {
                header = parts[0],
                payload = parts[1],
                tokenSig_head = parts[2][..Math.Min(10, parts[2].Length)],
                calcSig_head = calcSig[..Math.Min(10, calcSig.Length)],
                signatureEqual = equal,
                secretLen = secret.Length
            });
        }

    }
}
