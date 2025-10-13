using PureCosmetics.AuthService.Application.Models;
using PureCosmetics.AuthService.Application.Models.Requests.User;
using PureCosmetics.AuthService.Application.Models.Responses.User;
using PureCosmetics.AuthService.Application.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.ServiceImplements
{
    public class UserService : IUserService
    {
        public Task<ApiResponse<DataUserResponse>> CreateUser(UserCreateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
