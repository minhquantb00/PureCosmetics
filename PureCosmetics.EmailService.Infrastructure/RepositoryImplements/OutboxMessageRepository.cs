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
    public class OutboxMessageRepository : IOutboxMessageRepository
    {
        private readonly ApplicationDbContext _context;
        public OutboxMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(OutboxMessage outboxMessage)
        {
            _context.OutboxMessages.Add(outboxMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity =  await _context.OutboxMessages.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                _context.OutboxMessages.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IReadOnlyList<OutboxMessage>> GetUnpublishedAsync(int take, CancellationToken ct)
        {
            if (take <= 0) return Array.Empty<OutboxMessage>();

            var result = await _context.OutboxMessages
                .AsNoTracking()
                .Where(x => x.PublishedAt == null)
                .OrderBy(x => x.OccurredAt)
                .ThenBy(x => x.Id)
                .Take(take)
                .ToListAsync(ct);

            return result;
        }

        public async Task MarkPublishedAsync(long id, DateTime publishedAt, CancellationToken ct)
        {
            var entity = await _context.OutboxMessages.FirstOrDefaultAsync(x => x.Id == id, ct);
            if(entity != null)
            {
                entity.PublishedAt = publishedAt;
                _context.OutboxMessages.Update(entity);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task MarkPublishFailedAsync(long id, string error, CancellationToken ct)
        {
            var entity = await _context.OutboxMessages.FirstOrDefaultAsync(x => x.Id == id, ct);
            if (entity != null)
            {
                entity.AttemptCount += 1;
                entity.LastError = error;
                _context.OutboxMessages.Update(entity);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task UpdateAsync(OutboxMessage outboxMessage)
        {
            _context.OutboxMessages.Update(outboxMessage);
            await _context.SaveChangesAsync();
        }
    }
}
