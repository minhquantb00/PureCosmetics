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
        Task<IReadOnlyList<OutboxMessage>> GetUnpublishedAsync(int take, CancellationToken ct);

        Task MarkPublishedAsync(long id, DateTime publishedAt, CancellationToken ct);

        Task MarkPublishFailedAsync(long id, string error, CancellationToken ct);
    }
}
