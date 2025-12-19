using Microsoft.EntityFrameworkCore;
using PureCosmetics.AuthService.Domain.Entities;
using PureCosmetics.AuthService.Domain.RepositoryContracts;
using PureCosmetics.AuthService.Infrastructure.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Infrastructure.RepositoryImplements
{
    /// <summary>
    /// Implementation of Role Repository
    /// User create: QuanTM
    /// Created date: 2025/12/19
    /// Last modified date: 2025/12/19
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        #region Fields

        /// <summary>
        /// Application database context for accessing the database.
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Interface for database context operations.
        /// </summary>
        protected IDbContext _dbContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialize RoleRepository with database contexts.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="context"></param>
        public RoleRepository(IDbContext dbContext, ApplicationDbContext context)
        {
            _dbContext = dbContext;
            _context = context;
        }
        #endregion

        #region Writes

        /// <summary>
        /// Creates a new Role entity in the database.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task CreateAsyn(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing Role entity in the database.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a Role entity by its identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if(role == null)
            {
                return false;
            }
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }

        #endregion

        #region Reads

        /// <summary>
        /// Gets all Role entities, optionally filtered by a given expression.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IQueryable<Role>> GetAllAsync(Expression<Func<Role, bool>>? expression = null)
        {
            var query = await _context.Roles.ToListAsync();
            if(expression != null)
            {
                query = query.AsQueryable().Where(expression).ToList();
            }

            return query.AsQueryable();
        }

        /// <summary>
        /// Get by Id Role entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Role?> GetByIdAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            return role ?? null;
        }

        #endregion
    }
}
