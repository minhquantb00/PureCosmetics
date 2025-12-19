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
using PureCosmetics.Commons.Constants;
using PureCosmetics.Commons.Encrypts;
using PureCosmetics.Commons.HttpContext;
using PureCosmetics.Commons.Paginations;
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
    /// <summary>
    /// Functionality for user management
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last updated: 2025/12/11
    /// </summary>
    public class UserService : IUserService
    {
        #region Fields

        /// <summary>
        /// Repository for user entity
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Repository for refresh token entity
        /// </summary>
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        /// <summary>
        /// Interface for configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Provides access to the current HTTP context for the associated request.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for UserService
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="configuration"></param>
        /// <param name="refreshTokenRepository"></param>
        /// <param name="httpContextAccessor"></param>
        public UserService(IUserRepository userRepository, IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Writes
        /// <summary>
        /// Creates a new user account based on the specified request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<ApiResponse<DataUserResponse>> CreateUser(UserCreateRequest request)
        {
            UserValidate validator = new UserValidate();
            var validationResult = await validator.ValidateAsync(request);
            if(!validationResult.IsValid)
            {
                return ApiResponse<DataUserResponse>.Fail(MessageConstantForUser.VALIDATION_ERROR, HttpStatusCode.BadRequest, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            var existingUser = await _userRepository.GetAsync(u => u.PhoneNumber == request.PhoneNumber || u.Email == request.Email);
            if(existingUser != null)
            {
                return ApiResponse<DataUserResponse>.Fail(MessageConstantForUser.ALREADY_EXIST_EMAIL_OR_PHONENUMBER, HttpStatusCode.BadRequest, new List<string> { MessageConstantForUser.ALREADY_EXIST_EMAIL_OR_PHONENUMBER });
            }
            var listUser = await _userRepository.GetAllAsync();

            var lastNumericalOrder = listUser.Select(x => x.NumericalOrder).OrderBy(x => x).LastOrDefault();
            var user = new User(request.Email, request.PhoneNumber, request.UserName, request.Password, request.FirstName, request.LastName, request.DateOfBirth, lastNumericalOrder++, "https://hasaki.vn/images/graphics/account-full.svg",  null);

            await _userRepository.CreateAsyn(user);


            if(user == null)
            {
                throw new ArgumentNullException(MessageConstantForUser.USER_IS_NULL);
            }
            await _userRepository.AddRoleToUserAsync(user, new List<string> { Roles.ROLE_CUSTOMER });
            return ApiResponse<DataUserResponse>.Created(UserMapping.EntityToDto(user), MessageConstantForUser.USER_CREATED);
        }

        /// <summary>
        /// Implement logic update user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResponse<DataUserResponse>> UpdateUser(UserUpdateRequest request)
        {
            var validator = new UserUpdateValidate();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return ApiResponse<DataUserResponse>.Fail(MessageConstantForUser.VALIDATION_ERROR, HttpStatusCode.BadRequest, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            bool isAuthenticated = HttpContextHelper.IsUserAuthenticated(_httpContextAccessor);
            if (!isAuthenticated)
            {
                return ApiResponse<DataUserResponse>.Fail(MessageConstantForUser.UN_AUTHENTICATED, HttpStatusCode.Unauthorized, new List<string> { MessageConstantForUser.UN_AUTHENTICATED });
            }

            var user = await _userRepository.GetByIdAsync(request.Id);
            if(user == null)
            {
                return ApiResponse<DataUserResponse>.Fail(MessageConstantForUser.USER_IS_NULL, HttpStatusCode.BadRequest, new List<string> { MessageConstantForUser.USER_IS_NULL });
            }

            int currentUserId = HttpContextHelper.CurrentUserId(_httpContextAccessor);

            if (user.Id != currentUserId)
            {
                return ApiResponse<DataUserResponse>.Fail(MessageConstantForUser.UN_AUTHORIZED, HttpStatusCode.Forbidden, new List<string> { MessageConstantForUser.UN_AUTHORIZED });
            }

            user.Change(request.Id, request.Email, request.PhoneNumber, request.UserName, request.FirstName, request.LastName, request.DateOfBirth);

            await _userRepository.UpdateAsync(user);

            return ApiResponse<DataUserResponse>.Success(UserMapping.EntityToDto(user), MessageConstantForUser.USER_UPDATED);
        }

        /// <summary>
        /// Implement logic delete user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResponse<DataUserResponse>> DeleteUser(UserDeleteRequest request)
        {
            var currentUser = _httpContextAccessor.HttpContext!.User;
            if (!currentUser.Identity!.IsAuthenticated)
            {
                return ApiResponse<DataUserResponse>.Fail(MessageConstantForUser.UN_AUTHENTICATED, HttpStatusCode.Unauthorized, new List<string> { MessageConstantForUser.UN_AUTHENTICATED });
            }

            var user = await _userRepository.GetByIdAsync(request.Id);
            if(user == null)
            {
                return ApiResponse<DataUserResponse>.Fail(MessageConstantForUser.USER_IS_NULL, HttpStatusCode.BadRequest, new List<string> { MessageConstantForUser.USER_IS_NULL });
            }

            var currentUserId = int.Parse(currentUser.FindFirst("Id")!.Value);
            if(currentUserId != user.Id || !currentUser.IsInRole(Roles.ROLE_ADMIN))
            {
                return ApiResponse<DataUserResponse>.Fail(MessageConstantForUser.UN_AUTHORIZED, HttpStatusCode.Forbidden, new List<string> { MessageConstantForUser.UN_AUTHORIZED });
            }

            user.IsDeleted = true;
            user.DeletionTime = DateTime.Now;
            user.DeleterUserId = currentUserId;

            await _userRepository.UpdateAsync(user);

            return ApiResponse<DataUserResponse>.Success(UserMapping.EntityToDto(user), MessageConstantForUser.USER_DELETED);
        }

        /// <summary>
        /// Implement logic login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResponse<DataResponseLogin>> Login(UserLoginRequest request)
        {
            UserLoginValidate validator = new UserLoginValidate();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return ApiResponse<DataResponseLogin>.Fail(MessageConstantForUser.VALIDATION_ERROR, HttpStatusCode.BadRequest, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            var checkUserName = await _userRepository.GetAsync(u => u.UserName == request.UserName || u.Email == request.UserName);
            if(checkUserName == null)
            {
                return ApiResponse<DataResponseLogin>.Fail(MessageConstantForUser.INVALID_USERNAME_OR_EMAIL, HttpStatusCode.BadRequest, new List<string> { MessageConstantForUser.INVALID_USERNAME_OR_EMAIL });
            }

            var verifyPassword = BCrypt.Net.BCrypt.Verify(request.Password, checkUserName.PasswordHash);
            if (!verifyPassword)
            {
                return ApiResponse<DataResponseLogin>.Fail(MessageConstantForUser.INVALID_PASSWORD, HttpStatusCode.BadRequest, new List<string> { MessageConstantForUser.INVALID_PASSWORD });
            }

            var tokenResult = await GetJwtTokenAsync(checkUserName);

            return ApiResponse<DataResponseLogin>.Success(tokenResult, MessageConstantForUser.LOGIN_SUCCESS);
        }
        #endregion

        #region Reads

        /// <summary>
        /// Implement logic get all user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResponse<PagedResult<DataUserResponse>>> GetAllUsers(UserGetsRequest request)
        {
            var query = await _userRepository.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true);

            if(!string.IsNullOrWhiteSpace(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword) || x.Email.Contains(request.Keyword) || x.PhoneNumber.Contains(request.Keyword) || x.FirstName.Contains(request.Keyword) || x.LastName.Contains(request.Keyword));
            }

            var pagedResult = await PagedResult<DataUserResponse>.ToPagedResultAsync(
                new Pagination{Page = request.PageIndex, ItemsPerPage = request.PageSize},
                query.Select(UserMapping.EntityToDto).AsQueryable()
            );

            return pagedResult.Data != null
                ? ApiResponse<PagedResult<DataUserResponse>>.Success(pagedResult, "Users retrieved successfully.")
                : ApiResponse<PagedResult<DataUserResponse>>.Fail("Failed to retrieve users.", HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Implement logic get user by id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResponse<DataUserResponse>> GetUserById(UserGetByIdRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            return user != null
                ? ApiResponse<DataUserResponse>.Success(UserMapping.EntityToDto(user), "User retrieved successfully.")
                : ApiResponse<DataUserResponse>.Fail("User not found.", HttpStatusCode.NotFound);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get jwt token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private async Task<DataResponseLogin> GetJwtTokenAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var roles = _userRepository.GetRolesOfUserAsync(user);
            var permissions = await _userRepository.GetUserPermissionsAsync(user.Id);

            var claims = new List<Claim>
            {
                new("Id", user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            foreach (var perm in permissions)
            {
                claims.Add(new Claim("permission", perm));
            }

            var jwtToken = CreateJwt(claims);

            var refreshToken = GenerateRefreshToken();
            await _refreshTokenRepository.CreateAsync(new RefreshToken(user.Id, refreshToken, DateTime.UtcNow));

            return new DataResponseLogin
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                RefreshToken = refreshToken
            };
        }

        /// <summary>
        /// Create token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
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

        /// <summary>
        /// Render refresh token
        /// </summary>
        /// <returns></returns>
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
