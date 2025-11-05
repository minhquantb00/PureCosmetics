using PureCosmetics.AuthService.Application.Models;
using PureCosmetics.AuthService.Application.Models.Requests.User;
using PureCosmetics.AuthService.Application.Models.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.ServiceContracts
{
    public interface IUserService
    {
        Task<ApiResponse<DataUserResponse>> CreateUser(UserCreateRequest request);
        Task<ApiResponse<DataResponseLogin>> Login(UserLoginRequest request);
    }
}
