using PureCosmetics.EmailService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Domain.RepositoryContracts
{
    public interface IEmailProviderConfigRepository
    {
        Task CreateAsync(EmailProviderConfig emailProviderConfig);
        Task UpdateAsync(EmailProviderConfig emailProviderConfig);
        Task<bool> DeleteAsync(int id);
        Task<EmailProviderConfig?> GetDefaultAsync(CancellationToken ct);

        Task<EmailProviderConfig?> GetByNameAsync(string name, CancellationToken ct);

        Task<IReadOnlyList<EmailProviderConfig>> GetEnabledAsync(CancellationToken ct);

        Task SetDefaultAsync(int id, CancellationToken ct);

        Task UpdateSettingsAsync(int id, string settingsJson, CancellationToken ct);
    }
}
