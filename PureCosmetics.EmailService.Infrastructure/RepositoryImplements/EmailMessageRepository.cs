using Microsoft.EntityFrameworkCore;
using PureCosmetics.Commons.Enumerates;
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
    public class EmailMessageRepository : IEmailMessageRepository
    {
        private readonly ApplicationDbContext _context;
        public EmailMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(EmailMessage emailMessage)
        {
            _context.EmailMessages.Add(emailMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.EmailMessages.FirstOrDefaultAsync(x => x.Id == id);
            if(entity != null)
            {
                _context.EmailMessages.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ExistsByDedupAsync(string dedup, CancellationToken ct)
        {
            var check =  await _context.EmailMessages.AnyAsync(x => x.DeduplicationKey == dedup && x.Status != EmailStatusEnum.Failed, ct);
            return check;
        }

        public Task<List<EmailMessage>> TakeQueuedAsync(int take, DateTime utcNow, CancellationToken ct)
            => _context.EmailMessages
                  .Where(x => x.Status == EmailStatusEnum.Queued &&
                              (x.ScheduleAt == null || x.ScheduleAt <= utcNow))
                  .OrderByDescending(x => x.Priority).ThenBy(x => x.CreationTime)
                  .Take(take).ToListAsync(ct);

        public async Task UpdateAsync(EmailMessage emailMessage)
        {
            _context.EmailMessages.Update(emailMessage);
            await _context.SaveChangesAsync();
        }
    }
}
