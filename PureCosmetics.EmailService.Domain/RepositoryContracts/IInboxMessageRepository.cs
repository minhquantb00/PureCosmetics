using PureCosmetics.EmailService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PureCosmetics.EmailService.Domain.RepositoryContracts
{
    public interface IInboxMessageRepository
    {
        Task CreateAsync(InboxMessage inboxMessage);
        Task UpdateAsync(InboxMessage inboxMessage);
        Task<bool> DeleteAsync(int id);
        Task<bool> TryBeginAsync(string messageType, string dedupKey, string correlationId, string payloadJson, CancellationToken ct);

        Task MarkProcessedAsync(string dedupKey, CancellationToken ct);

        Task MarkFailedAsync(string dedupKey, string error, CancellationToken ct);

        Task<IReadOnlyList<InboxMessage>> GetFailedAsync(string take, CancellationToken ct);
    }
}
