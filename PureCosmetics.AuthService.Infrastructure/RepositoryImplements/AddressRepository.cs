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
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        protected readonly IDbContext _dbContext;
        public AddressRepository(ApplicationDbContext context, IDbContext dbContext)
        {
            _context = context;
            _dbContext = dbContext;
        }
        #region Write
        public async Task CreateAsyn(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.Addresses.FindAsync(id);
            if (data != null)
            {
                _context.Addresses.Remove(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Address, bool>> prodecate)
        {
            var data = await _context.Addresses.FirstOrDefaultAsync(prodecate);
            if (data != null)
            {
                _context.Addresses.Remove(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeleteRangeAsync(IEnumerable<Address> entities)
        {
            if (entities.Any())
            {
                foreach (var entity in entities)
                {
                    _context.Addresses.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
        }
        #endregion
        #region Read
        public async Task<IQueryable<Address>> GetAllAsync(Expression<Func<Address, bool>>? expression = null)
        {
            var query = _context.Addresses.AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return await Task.FromResult(query);
        }

        public async Task<Address?> GetAsync(Expression<Func<Address, bool>>? predicate = null)
        {
            var data = await _context.Addresses.FirstOrDefaultAsync(predicate!);
            if (data == null)
            {
                return null;
            }
            return data;
        }

        public Address? GetById(int id)
        {
            var data = _context.Addresses.Find(id);
            if (data == null)
            {
                return null;
            }
            return data;
        }

        public async Task<Address?> GetByIdAsync(int id)
        {
            var data = await _context.Addresses.FindAsync(id);
            if (data == null)
            {
                return null;
            }
            return data;
        }
        #endregion
    }
}
