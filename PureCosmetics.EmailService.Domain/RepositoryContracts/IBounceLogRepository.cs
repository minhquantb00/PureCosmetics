using PureCosmetics.EmailService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Domain.RepositoryContracts
{
    public interface IBounceLogRepository
    {
        Task CreateAsync(BounceLog bounceLog);
        Task UpdateAsync(BounceLog bounceLog);
        Task<bool> DeleteAsync(int id);
    }
}
