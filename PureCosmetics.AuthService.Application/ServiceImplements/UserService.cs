using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PureCosmetics.AuthService.Application.Mappers;
using PureCosmetics.AuthService.Application.Models;
using PureCosmetics.AuthService.Application.Models.Requests.User;
using PureCosmetics.AuthService.Application.Models.Responses.User;
using PureCosmetics.AuthService.Application.ServiceContracts;
using PureCosmetics.AuthService.Application.Validators;
using PureCosmetics.AuthService.Domain.Entities;
using PureCosmetics.AuthService.Domain.RepositoryContracts;
using PureCosmetics.Commons.Encrypts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.ServiceImplements
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructors
        public UserService(IUserRepository userRepository, IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion
        #region Writes
        public async Task<ApiResponse<DataUserResponse>> CreateUser(UserCreateRequest request)
        {
            UserValidate validator = new UserValidate();
            var validationResult = await validator.ValidateAsync(request);
            if(!validationResult.IsValid)
            {
                return new ApiResponse<DataUserResponse>
                {
                    IsSuccess = false,
                    Message = "Validation errors",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var existingUser = await _userRepository.GetAsync(u => u.PhoneNumber == request.PhoneNumber || u.Email == request.Email);
            if(existingUser != null)
            {
                return new ApiResponse<DataUserResponse>
                {
                    IsSuccess = false,
                    Message = "User with the same PhoneNumber or Email already exists.",
                    Errors = new List<string> { "Duplicate PhoneNumber or Email." }
                };
            }
            var listUser = await _userRepository.GetAllAsync();

            var lastNumericalOrder = listUser.Select(x => x.NumericalOrder).Max();
            var user = new User(request.Email, request.PhoneNumber, request.UserName, request.Password, request.FirstName, request.LastName, request.DateOfBirth, lastNumericalOrder++,  null);

            await _userRepository.CreateAsyn(user);


            if(user == null)
            {
                throw new ArgumentNullException("User is null");
            }
            await _userRepository.AddRoleToUserAsync(user, new List<string> { "ROLE_CUSTOMER" });
            return new ApiResponse<DataUserResponse>
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                TimeStamp = DateTime.Now,
                Message = "Account created successfully!",
                Data = UserMapping.EntityToDto(user)
            };
        }

        public async Task<ApiResponse<DataUserResponse>> UpdateUser(UserUpdateRequest request)
        {
            var validator = new UserUpdateValidate();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return new ApiResponse<DataUserResponse>
                {
                    IsSuccess = false,
                    Message = "Validation errors",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            ClaimsPrincipal currentUser = _httpContextAccessor.HttpContext!.User;
            if (!currentUser.Identity!.IsAuthenticated)
            {
                return new ApiResponse<DataUserResponse>
                {
                    Data = null,
                    Errors = new List<string> { "UnAuthenticated user" },
                    IsSuccess = false,
                    Message = "UnAuthenticated user",
                    StatusCode = HttpStatusCode.Unauthorized,
                    TimeStamp = DateTime.Now,
                };
            }

            var user = await _userRepository.GetByIdAsync(request.Id);
            if(user == null)
            {
                return new ApiResponse<DataUserResponse>
                {
                    IsSuccess = false,
                    Message = "User is null",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            int currentUserId = int.Parse(currentUser.FindFirst("Id")!.Value);
            if (user.Id != currentUserId)
            {
                return new ApiResponse<DataUserResponse>
                {
                    Data = null,
                    Errors = new List<string> { "The user does not have permission to perform this function" },
                    IsSuccess = false,
                    Message = "The user does not have permission to perform this function",
                    StatusCode= HttpStatusCode.Forbidden,
                };
            }

            user.Change(request.Id, request.Email, request.PhoneNumber, request.UserName, request.FirstName, request.LastName, request.DateOfBirth);

            await _userRepository.UpdateAsync(user);

            return new ApiResponse<DataUserResponse>
            {
                Data = UserMapping.EntityToDto(user),
                IsSuccess = true,
                Message = "Account updated successfully!",
                StatusCode = HttpStatusCode.OK,
                TimeStamp = DateTime.Now
            };
        }

        public async Task<ApiResponse<DataResponseLogin>> Login(UserLoginRequest request)
        {
            UserLoginValidate validator = new UserLoginValidate();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return new ApiResponse<DataResponseLogin>
                {
                    IsSuccess = false,
                    Message = "Validation errors",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var checkUserName = await _userRepository.GetAsync(u => u.UserName == request.UserName || u.Email == request.UserName);
            if(checkUserName == null)
            {
                return new ApiResponse<DataResponseLogin>
                {
                    IsSuccess = false,
                    Message = "User not found.",
                    Errors = new List<string> { "Invalid username or email." }
                };
            }

            var verifyPassword = BCrypt.Net.BCrypt.Verify(request.Password, checkUserName.PasswordHash);
            if (!verifyPassword)
            {
                return new ApiResponse<DataResponseLogin>
                {
                    IsSuccess = false,
                    Message = "Incorrect password.",
                    Errors = new List<string> { "Invalid password." }
                };
            }

            var tokenResult = await GetJwtTokenAsync(checkUserName);

            return new ApiResponse<DataResponseLogin>
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                TimeStamp = DateTime.Now,
                Message = "Login successful!",
                Data = tokenResult
            };
        }
        #endregion
        #region Private Methods
        private async Task<DataResponseLogin> GetJwtTokenAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var roles = await _userRepository.GetUserRolesAsync(user.Id);
            var permissions = await _userRepository.GetUserPermissionsAsync(user.Id);

            var claims = new List<Claim>
            {
                new("Id", user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            foreach (var perm in permissions)
                claims.Add(new Claim("permission", perm));

            var jwtToken = CreateJwt(claims);

            var refreshToken = GenerateRefreshToken();
            await _refreshTokenRepository.CreateAsync(new RefreshToken(user.Id, refreshToken, DateTime.UtcNow));

            return new DataResponseLogin
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                RefreshToken = refreshToken
            };
        }

        private JwtSecurityToken CreateJwt(List<Claim> claims)
        {
            var secret = (_configuration["JWT:SecretKey"] ?? "").Trim();
            if (string.IsNullOrWhiteSpace(secret))
            {
                throw new InvalidOperationException("JWT:SecretKey is missing.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            _ = int.TryParse(_configuration["JWT:TokenValidityInHours"], out int hours);
            if (hours <= 0) hours = 1;

            var now = DateTime.UtcNow;

            return new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                notBefore: now,
                expires: now.AddHours(hours),
                signingCredentials: creds
            );
        }


        private string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        #endregion
    }
}
