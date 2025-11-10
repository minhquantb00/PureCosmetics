using PureCosmetics.EmailService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Domain.RepositoryContracts
{
    public interface IOutboxMessageRepository
    {
        Task CreateAsync(OutboxMessage outboxMessage);
        Task UpdateAsync(OutboxMessage outboxMessage);
        Task<bool> DeleteAsync(int id);
    }
}
