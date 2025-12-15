using PureCosmetics.AuthService.Application.Models;
using PureCosmetics.AuthService.Application.Models.Requests.Role;
using PureCosmetics.AuthService.Application.Models.Responses.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.ServiceContracts
{
    /// <summary>
    /// Interface for Role Service
    /// User create: QuanTM
    /// Created date: 2025/12/15
    /// Last modified date: 2025/12/15
    /// </summary>
    [ServiceContract]
    public interface IRoleService
    {
        /// <summary>
        /// Create a new role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        Task<ApiResponse<DataResponseRole>> CreateRole(RoleCreateRequest request);

        /// <summary>
        /// Update an existing role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        Task<ApiResponse<DataResponseRole>> UpdateRole(RoleChangeRequest request);

        /// <summary>
        /// Delete a role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        Task<ApiResponse<DataResponseRole>> DeleteRole(RoleRemoveRequest request);

        /// <summary>
        /// Get role by id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        Task<DataResponseRole> GetRoleById(RoleGetRequest request);

        /// <summary>
        /// Get list of roles with pagination and filtering
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        Task<List<DataResponseRole>> GetRoles(RoleGetsRequest request);
    }
}
