using PureCosmetics.AuthService.Application.Models;
using PureCosmetics.AuthService.Application.Models.Requests.User;
using PureCosmetics.AuthService.Application.Models.Responses.User;
using PureCosmetics.Commons.Paginations;
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
        [OperationContract]
        Task<ApiResponse<DataUserResponse>> DeleteUser(UserDeleteRequest request);
        [OperationContract]
        Task<ApiResponse<PagedResult<DataUserResponse>>> GetAllUsers(UserGetsRequest request);
        [OperationContract]
        Task<ApiResponse<DataUserResponse>> GetUserById(UserGetByIdRequest request);
    }
}
