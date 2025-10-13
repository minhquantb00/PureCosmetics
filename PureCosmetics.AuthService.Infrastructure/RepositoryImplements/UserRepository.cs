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
        public async Task CreateAsyn(User user)
        {
            _context.Users.Add(user);
            await _dbContext.CommitChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.Users.FindAsync(id);
            if(data != null)
            {
                data.IsDeleted = true;
                data.DeletionTime = DateTime.Now;
                _context.Users.Update(data);
                await _dbContext.CommitChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> prodecate)
        {
            var data = await _context.Users.FirstOrDefaultAsync(prodecate);
            if(data != null)
            {
                data.IsDeleted = true;
                data.DeletionTime = DateTime.Now;
                _context.Users.Update(data);
                await _dbContext.CommitChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeleteRangeAsync(IEnumerable<User> entities)
        {
            if (entities.Any())
            {
                foreach(var entity in entities)
                {
                    entity.IsDeleted = true;
                    entity.DeletionTime = DateTime.Now;
                    _context.Users.Update(entity);
                    await _dbContext.CommitChangesAsync();
                }
            }
        }

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
            if(data == null)
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

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _dbContext.CommitChangesAsync();
        }
    }
}
