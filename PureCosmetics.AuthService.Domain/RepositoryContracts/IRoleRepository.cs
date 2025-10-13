using PureCosmetics.AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Domain.RepositoryContracts
{
    public interface IRoleRepository
    {
        Task<IQueryable<Role>> GetAllAsync(Expression<Func<Role, bool>>? expression = null);
    }
}
