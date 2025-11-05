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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        protected IDbContext _dbContext;
        public UserRepository(IDbContext dbContext, ApplicationDbContext context)
        {
            _dbContext = dbContext;
            _context = context;
        }
        #region Handle Role
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
        public IEnumerable<string> GetRolesOfUserAsync(User user)
        {
            List<string> roles = new List<string>();
            var listRoles = _context.UserRoles.Where(x => x.UserId == user.Id).AsQueryable();
            foreach (var item in listRoles.Distinct())
            {
                var role = _context.Roles.SingleOrDefault(x => x.Id == item.RoleId);
                if (role != null)
                {
                    roles.Add(role.Code);
                }
            }
            return roles.AsEnumerable();
        }
        #endregion
        #region Handle String
        private Task<bool> CompareStringAsync(string str1, string str2)
        {
            return Task.FromResult(string.Equals(str1.ToLowerInvariant(), str2.ToLowerInvariant()));
        }



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
        #region Write
        public async Task CreateAsyn(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
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
        #region Read
        public async Task<IQueryable<User>> GetAllAsync(Expression<Func<User, bool>>? expression = null)
        {
            var query = _context.Users.AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return await Task.FromResult(query);
        }

        public async Task<User?> GetAsync(Expression<Func<User, bool>>? predicate = null)
        {
            var data = await _context.Users.FirstOrDefaultAsync(predicate!);
            if (data == null)
            {
                return null;
            }
            return data;
        }

        public User? GetById(int id)
        {
            var data = _context.Users.Find(id);
            if (data == null)
            {
                return null;
            }
            return data;
        }

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
