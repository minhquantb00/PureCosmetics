using PureCosmetics.AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Domain.RepositoryContracts
{
    /// <summary>
    /// Interface for Role Repository
    /// User create: QuanTM
    /// Created date: 2025/12/19
    /// Last modified date: 2025/12/19
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Get all roles with optional filtering
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<IQueryable<Role>> GetAllAsync(Expression<Func<Role, bool>>? expression = null);

        /// <summary>
        /// Create a new role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task CreateAsyn(Role role);

        /// <summary>
        /// Update an existing role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task UpdateAsync(Role role);

        /// <summary>
        /// Delete a role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Get a role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Role?> GetByIdAsync(int id);
    }
}
