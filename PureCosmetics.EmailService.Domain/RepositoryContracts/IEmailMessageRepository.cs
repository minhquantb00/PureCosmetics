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
        Task<EmailMessage?> FindByMessageIdAsync(Guid messageId, CancellationToken ct);
        Task<EmailMessage?> FindByDedupKeyAsync(string dedupKey, CancellationToken ct);
        Task<List<EmailMessage>> TakeQueuedAsync(int take, DateTime utcNow, CancellationToken ct);
        Task AddAttachmentsAsync(int emailId, IEnumerable<EmailAttachment> attachments, CancellationToken ct);
        Task<bool> TryMarkSendingAsync(int emailId, CancellationToken ct);
        Task<int> BulkMarkDeadLetterAsync(DateTime nowUtc, CancellationToken ct);
    }
}
