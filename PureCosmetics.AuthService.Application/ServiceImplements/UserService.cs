using PureCosmetics.AuthService.Application.Models;
using PureCosmetics.AuthService.Application.Models.Requests.User;
using PureCosmetics.AuthService.Application.Models.Responses.User;
using PureCosmetics.AuthService.Application.ServiceContracts;
using PureCosmetics.AuthService.Application.Validators;
using PureCosmetics.AuthService.Domain.Entities;
using PureCosmetics.AuthService.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.ServiceImplements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

            var user = new User(request.Email, request.PhoneNumber, request.UserName, request.Password, request.FirstName, request.LastName, request.DateOfBirth, null) { Id = 0};

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
    }
}
