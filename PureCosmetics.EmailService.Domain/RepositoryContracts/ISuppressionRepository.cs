using PureCosmetics.EmailService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Domain.RepositoryContracts
{
    public interface ISuppressionRepository
    {
        Task CreateAsync(Suppression suppression);
        Task UpdateAsync(Suppression suppression);
        Task<bool> DeleteAsync(int id);
    }
}
