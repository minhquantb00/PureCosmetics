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
        Task<bool> IsSuppressedAsync(string email, CancellationToken ct);
        Task<bool> IsSuppressedAsync(string email, DateTime nowUtc, CancellationToken ct);

        Task UpsertAsync(string email, string reason, string source, DateTime occurredAt, DateTime expiresAt, CancellationToken ct);

        Task RemoveAsync(string email, CancellationToken ct);

        Task<IReadOnlyList<Suppression>> SearchAsync( string? reason, CancellationToken ct);
    }
}
