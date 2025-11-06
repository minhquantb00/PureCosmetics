using PureCosmetics.AuthService.Application.Models;
using PureCosmetics.AuthService.Application.Models.Requests.User;
using PureCosmetics.AuthService.Application.Models.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.ServiceContracts
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        Task<ApiResponse<DataUserResponse>> CreateUser(UserCreateRequest request);
        [OperationContract]
        Task<ApiResponse<DataResponseLogin>> Login(UserLoginRequest request);
        [OperationContract]
        Task<ApiResponse<DataUserResponse>> UpdateUser(UserUpdateRequest request);
    }
}
