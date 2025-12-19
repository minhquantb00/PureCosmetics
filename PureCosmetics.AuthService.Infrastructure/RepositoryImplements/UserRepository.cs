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
    /// Implementation of User Repository
    /// User create: QuanTM
    /// Created date: 2025/12/19
    /// Last modified date: 2025/12/19
    /// </summary>
    public class UserRepository : IUserRepository
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
        /// Constructor to initialize UserRepository with database contexts.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="context"></param>
        public UserRepository(IDbContext dbContext, ApplicationDbContext context)
        {
            _dbContext = dbContext;
            _context = context;
        }

        #endregion

        #region Handle Role

        /// <summary>
        /// Add role to user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="listRoles"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task AddRoleToUserAsync(User user, List<string> listRoles)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (listRoles == null)
            {
                throw new ArgumentNullException(nameof(listRoles));
            }
            foreach (var role in listRoles.Distinct())
            {
                var rolesOfUser =  GetRolesOfUserAsync(user);
                if (await IsStringInListAsync(role, [.. rolesOfUser]))
                {
                    throw new ArgumentException("The user already has this permission");
                }
                else
                {
                    var roleItem = await _context.Roles.FirstOrDefaultAsync(x => x.Code.Equals(role));
                    if (roleItem == null)
                    {
                        throw new ArgumentNullException("Role is null");
                    }
                    _context.UserRoles.Add(new UserRole(user.Id, roleItem.Id));
                }
            }
             _context.SaveChanges();
        }

        /// <summary>
        /// Delete list role of user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="listRoles"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task DeleteRolesOfUserAsync(User user, List<string> listRoles)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (listRoles == null)
            {
                throw new ArgumentNullException(nameof(listRoles));
            }
            foreach (var role in listRoles.Distinct())
            {
                var rolesOfUser = GetRolesOfUserAsync(user);
                var listPermission = new List<UserRole>();
                if (await IsStringInListAsync(role, rolesOfUser.ToList()))
                {
                    var roleItem = await _context.Roles.SingleOrDefaultAsync(x => x.Code.Equals(role));
                    if (roleItem == null)
                    {
                        throw new ArgumentNullException("Role is null");
                    }
                    var permission = await _context.UserRoles.FirstOrDefaultAsync(x => x.UserId == user.Id && x.RoleId == roleItem.Id);
                    if (permission != null)
                    {
                        listPermission.Add(permission);
                    }
                }
                else
                {

                    throw new ArgumentNullException("Role is null");

                }
                _context.UserRoles.RemoveRange(listPermission);
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get list roles of user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<string> GetRolesOfUserAsync(User user)
        {
            List<string> roles = new List<string>();
            var listRoles = _context.UserRoles.Where(x => x.UserId == user.Id).AsQueryable();
            var roleIds = listRoles.Select(x => x.RoleId).Distinct().ToList();
            var roleData = _context.Roles.Where(x => roleIds.Contains(x.Id)).ToList();
            foreach (var item in roleData)
            {
                if (item != null)
                {
                    roles.Add(item.Code);
                }
            }
            return roles.AsEnumerable();
        }

        /// <summary>
        /// Get user roles by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetUserRolesAsync(int userId)
        => await (from ur in _context.UserRoles
                  join r in _context.Roles on ur.RoleId equals r.Id
                  where ur.UserId == userId
                  select r.Code)
            .Distinct().ToListAsync();

        /// <summary>
        /// Get user permissions by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetUserPermissionsAsync(int userId)
            => await (from ur in _context.UserRoles
                      join rp in _context.RolePermissions on ur.RoleId equals rp.RoleId
                      join p in _context.Permissions on rp.PermissionId equals p.Id
                      where ur.UserId == userId
                      select p.Code)
                .Distinct().ToListAsync();
        #endregion

        #region Handle String

        /// <summary>
        /// Compare two string ignore case
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns></returns>
        private Task<bool> CompareStringAsync(string str1, string str2)
        {
            return Task.FromResult(string.Equals(str1.ToLowerInvariant(), str2.ToLowerInvariant()));
        }

        /// <summary>
        /// Is string in list string
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="listString"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private async Task<bool> IsStringInListAsync(string inputString, List<string> listString)
        {
            if (inputString == null)
            {
                throw new ArgumentNullException(nameof(inputString));
            }

            if (listString == null)
            {
                throw new ArgumentNullException(nameof(listString));
            }

            foreach (var str in listString)
            {
                if (await CompareStringAsync(inputString, str))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Writes

        /// <summary>
        /// Create a new user asynchronously.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task CreateAsyn(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update an existing user asynchronously.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete a user by its ID asynchronously (soft delete).
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.Users.FindAsync(id);
            if (data != null)
            {
                data.IsDeleted = true;
                data.DeletionTime = DateTime.Now;
                _context.Users.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Delete a user by a specified predicate asynchronously (soft delete).
        /// </summary>
        /// <param name="prodecate"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> prodecate)
        {
            var data = await _context.Users.FirstOrDefaultAsync(prodecate);
            if (data != null)
            {
                data.IsDeleted = true;
                data.DeletionTime = DateTime.Now;
                _context.Users.Update(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Delete a range of users asynchronously (soft delete).
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task DeleteRangeAsync(IEnumerable<User> entities)
        {
            if (entities.Any())
            {
                foreach (var entity in entities)
                {
                    entity.IsDeleted = true;
                    entity.DeletionTime = DateTime.Now;
                    _context.Users.Update(entity);
                    await _context.SaveChangesAsync();
                }
            }
        }
        #endregion

        #region Reads

        /// <summary>
        /// Get all users asynchronously with optional filtering.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<IQueryable<User>> GetAllAsync(Expression<Func<User, bool>>? expression = null)
        {
            var query = _context.Users.AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return await Task.FromResult(query);
        }

        /// <summary>
        /// Get a single user asynchronously based on a specified predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<User?> GetAsync(Expression<Func<User, bool>>? predicate = null)
        {
            var data = await _context.Users.FirstOrDefaultAsync(predicate!);
            if (data == null)
            {
                return null;
            }
            return data;
        }

        /// <summary>
        /// Get a user by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User? GetById(int id)
        {
            var data = _context.Users.Find(id);
            if (data == null)
            {
                return null;
            }
            return data;
        }

        /// <summary>
        /// Get a user by its ID asynchronously.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User?> GetByIdAsync(int id)
        {
            var data = await _context.Users.FindAsync(id);
            if (data == null)
            {
                return null;
            }
            return data;
        }
        #endregion
    }
}
