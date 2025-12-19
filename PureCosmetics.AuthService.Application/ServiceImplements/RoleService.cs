using PureCosmetics.AuthService.Application.Models;
using PureCosmetics.AuthService.Application.Models.Requests.Role;
using PureCosmetics.AuthService.Application.Models.Responses.Role;
using PureCosmetics.AuthService.Application.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.ServiceImplements
{
    /// <summary>
    /// Implementation of Role Service
    /// User create: QuanTM
    /// Created date: 2025/12/19
    /// Last modified date: 2025/12/19
    /// </summary>
    public class RoleService : IRoleService
    {
        #region Fields
        
        #endregion

        public Task<ApiResponse<DataResponseRole>> CreateRole(RoleCreateRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<DataResponseRole>> DeleteRole(RoleRemoveRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponseRole> GetRoleById(RoleGetRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<List<DataResponseRole>> GetRoles(RoleGetsRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<DataResponseRole>> UpdateRole(RoleChangeRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
