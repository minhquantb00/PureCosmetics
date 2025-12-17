using PureCosmetics.EmailService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Domain.RepositoryContracts
{
    public interface IEmailMessageRepository
    {
        Task CreateAsync(EmailMessage emailMessage);
        Task UpdateAsync(EmailMessage emailMessage);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsByDedupAsync(string dedup, CancellationToken ct);
        Task<List<EmailMessage>> TakeQueuedAsync(int take, DateTime utcNow, CancellationToken ct);
    }
}
