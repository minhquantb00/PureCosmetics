using PureCosmetics.AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Domain.RepositoryContracts
{
    public interface IAddressRepository 
    {
        Task<Address?> GetByIdAsync(int id);
        Address? GetById(int id);
        Task<Address?> GetAsync(Expression<Func<Address, bool>>? predicate = null);
        Task<IQueryable<Address>> GetAllAsync(Expression<Func<Address, bool>>? expression = null);
        Task CreateAsyn(Address address);
        Task UpdateAsync(Address address);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(Expression<Func<Address, bool>> prodecate);
        Task DeleteRangeAsync(IEnumerable<Address> entities);
    }
}
