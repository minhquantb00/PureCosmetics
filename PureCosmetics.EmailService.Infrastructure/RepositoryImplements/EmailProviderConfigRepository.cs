using Microsoft.EntityFrameworkCore;
using PureCosmetics.EmailService.Domain.Entities;
using PureCosmetics.EmailService.Domain.RepositoryContracts;
using PureCosmetics.EmailService.Infrastructure.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Infrastructure.RepositoryImplements
{
    public class EmailProviderConfigRepository : IEmailProviderConfigRepository
    {
        private readonly ApplicationDbContext _context;
        public EmailProviderConfigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(EmailProviderConfig emailProviderConfig)
        {
            _context.EmailProviderConfigs.Add(emailProviderConfig);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.EmailProviderConfigs.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                _context.EmailProviderConfigs.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<EmailProviderConfig?> GetByNameAsync(string name, CancellationToken ct)
        {
            var entity = await _context.EmailProviderConfigs
                .FirstOrDefaultAsync(x => x.Name == name, ct);

            return entity;
        }

        public Task<EmailProviderConfig?> GetDefaultAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<EmailProviderConfig>> GetEnabledAsync(CancellationToken ct)
        {
            var result = await _context.EmailProviderConfigs
                .Where(x => x.Enabled)
                .ToListAsync(ct);

            return result;
        }

        public async Task SetDefaultAsync(int id, CancellationToken ct)
        {
            var entity = await _context.EmailProviderConfigs
                .FirstOrDefaultAsync(x => x.Id == id, ct);
            if(entity == null)
            {
                throw new Exception($"EmailProviderConfig with Id {id} not found.");
            }

            entity.IsDefault = true;
            _context.EmailProviderConfigs.Update(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(EmailProviderConfig emailProviderConfig)
        {
            _context.EmailProviderConfigs.Update(emailProviderConfig);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSettingsAsync(int id, string settingsJson, CancellationToken ct)
        {
            var entity = await _context.EmailProviderConfigs
                .FirstOrDefaultAsync(x => x.Id == id, ct);
            if (entity == null)
            {
                throw new Exception($"EmailProviderConfig with Id {id} not found.");
            }

            entity.SettingsJson = settingsJson;
            _context.EmailProviderConfigs.Update(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
