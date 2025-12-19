using PureCosmetics.AuthService.Application.Mappers;
using PureCosmetics.AuthService.Application.Models;
using PureCosmetics.AuthService.Application.Models.Requests.Role;
using PureCosmetics.AuthService.Application.Models.Responses.Role;
using PureCosmetics.AuthService.Application.ServiceContracts;
using PureCosmetics.AuthService.Domain.Entities;
using PureCosmetics.AuthService.Domain.RepositoryContracts;
using PureCosmetics.Commons.Constants;
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

        /// <summary>
        /// Interface for Role Repository operations.
        /// </summary>
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// Interface for User Repository operations.
        /// </summary>
        private readonly IUserRepository _userRepository;
        #endregion

        #region Constructor
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roleRepository"></param>
        /// <param name="userRepository"></param>
        public RoleService(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }
        #endregion

        #region Writes

        /// <summary>
        /// Create a new role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResponse<DataResponseRole>> CreateRole(RoleCreateRequest request)
        {
            var roleExists = await _roleRepository.GetAllAsync(x => x.Code.Equals(request.Code));
            if(roleExists.Any())
            {
                return ApiResponse<DataResponseRole>.Fail(MessageConstantForRole.ROLE_CODE_ALREADY_EXISTS, System.Net.HttpStatusCode.Conflict);
            }

            var role = new Role(request.Name, request.Code, request.Description);
            if(role == null)
            {
                return ApiResponse<DataResponseRole>.Fail(MessageConstantForRole.ROLE_CREATION_FAILED, System.Net.HttpStatusCode.BadRequest);
            }
            await _roleRepository.CreateAsyn(role);

            var dto = RoleMapping.EntityToDto(role);
            return ApiResponse<DataResponseRole>.Created(dto, MessageConstantForRole.ROLE_CREATED_SUCCESSFULLY);
        }

        /// <summary>
        /// Update an existing role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResponse<DataResponseRole>> UpdateRole(RoleChangeRequest request)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id);
            if(role == null)
            {
                return ApiResponse<DataResponseRole>.Fail(MessageConstantForRole.ROLE_NOT_FOUND, System.Net.HttpStatusCode.NotFound);
            }

            var roleWithSameCode = await _roleRepository.GetAllAsync(x => x.Code.Equals(request.Code));
            if (roleWithSameCode.Any())
            {
                return ApiResponse<DataResponseRole>.Fail(MessageConstantForRole.ROLE_CODE_ALREADY_EXISTS, System.Net.HttpStatusCode.Conflict);
            }

            role.Change(request.Name, request.Code, request.Description);

            await _roleRepository.UpdateAsync(role);

            var dto = RoleMapping.EntityToDto(role);

            return ApiResponse<DataResponseRole>.Success(dto, MessageConstantForRole.ROLE_UPDATED_SUCCESSFULLY);
        }

        /// <summary>
        /// Delete a role
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResponse<DataResponseRole>> DeleteRole(RoleRemoveRequest request)
        {
            var role = await _roleRepository.GetByIdAsync(request.Id);
            if(role == null)
            {
                return ApiResponse<DataResponseRole>.Fail(MessageConstantForRole.ROLE_NOT_FOUND, System.Net.HttpStatusCode.NotFound);
            }

            await _roleRepository.DeleteAsync(role.Id);

            var dto = RoleMapping.EntityToDto(role);
            return ApiResponse<DataResponseRole>.Success(dto, MessageConstantForRole.ROLE_DELETED_SUCCESSFULLY);
        }

        #endregion

        #region Reads

        /// <summary>
        /// Get role by Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<DataResponseRole> GetRoleById(RoleGetRequest request)
        {
            var role =  await _roleRepository.GetByIdAsync(request.Id);
            if(role == null)
            {
                return new DataResponseRole();
            }

            var dto = RoleMapping.EntityToDto(role);
            return dto;
        }

        /// <summary>
        /// Get list of roles
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<DataResponseRole>> GetRoles(RoleGetsRequest request)
        {
            var query = await _roleRepository.GetAllAsync();
            var filteredQuery = query.ToList();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                filteredQuery = filteredQuery.Where(x => x.Name.Contains(request.Keyword) || x.Code.Contains(request.Keyword)).ToList();
            }

            var dtos = filteredQuery.Select(role => RoleMapping.EntityToDto(role)).ToList();

            return dtos;
        }
        #endregion
    }
}
