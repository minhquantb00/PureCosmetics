using PureCosmetics.AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Domain.RepositoryContracts
{
    public interface IUserRepository
    {
        Task AddRoleToUserAsync(User user, List<string> listRoles);
        Task DeleteRolesOfUserAsync(User user, List<string> listRoles);
        IEnumerable<string> GetRolesOfUserAsync(User user);
        Task<User?> GetByIdAsync(int id);
        User? GetById(int id);
        Task<User?> GetAsync(Expression<Func<User, bool>>? predicate = null);
        Task<IQueryable<User>> GetAllAsync(Expression<Func<User, bool>>? expression = null);
        Task CreateAsyn(User user);
        Task UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(Expression<Func<User, bool>> prodecate);
        Task DeleteRangeAsync(IEnumerable<User> entities);
        Task<IEnumerable<string>> GetUserRolesAsync(int userId);
        Task<IEnumerable<string>> GetUserPermissionsAsync(int userId);
    }
}
