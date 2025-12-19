using PureCosmetics.EmailService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Domain.RepositoryContracts
{
    public interface IInboxMessageRepository
    {
        Task CreateAsync(InboxMessage inboxMessage);
        Task UpdateAsync(InboxMessage inboxMessage);
        Task<bool> DeleteAsync(int id);
    }
}
