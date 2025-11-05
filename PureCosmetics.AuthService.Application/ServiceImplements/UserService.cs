using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
        }
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


            if(user != null)
            {
                await _userRepository.AddRoleToUserAsync(user, new List<string> { "ROLE_CUSTOMER" });
            }
            
            return new ApiResponse<DataUserResponse>
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                TimeStamp = DateTime.Now,
                Message = "Account created successfully!"
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
        #region Private Methods
        private async Task<DataResponseLogin> GetJwtTokenAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var claims = new List<Claim>
            {
                new("Id", user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            };

            string accessToken = CreateJwt(claims);
            string refreshToken = GenerateRefreshToken();

            await _refreshTokenRepository.CreateAsync(new RefreshToken(user.Id, refreshToken, DateTime.UtcNow));

            return new DataResponseLogin
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string CreateJwt(IEnumerable<Claim> claims)
        {
            var secret = _configuration["Jwt:Secret"]?.Trim();
            if (string.IsNullOrWhiteSpace(secret))
                throw new InvalidOperationException("Jwt:Secret is missing.");

            var keyBytes = Encoding.UTF8.GetBytes(secret);
            var signingKey = new SymmetricSecurityKey(keyBytes);

            _ = int.TryParse(_configuration["Jwt:TokenValidityInHours"], out int hours);
            if (hours <= 0) hours = 1;

            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                claims: claims,
                notBefore: now,
                expires: now.AddHours(hours),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
