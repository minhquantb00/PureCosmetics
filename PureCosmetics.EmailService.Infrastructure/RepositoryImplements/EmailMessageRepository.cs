using Microsoft.EntityFrameworkCore;
using PureCosmetics.Commons.Enumerates;
using PureCosmetics.EmailService.Domain.Entities;
using PureCosmetics.EmailService.Domain.RepositoryContracts;
using PureCosmetics.EmailService.Infrastructure.ORM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public async Task AddAttachmentsAsync(int emailId, IEnumerable<EmailAttachment> attachments, CancellationToken ct)
        {
            var email = await _context.EmailMessages.FirstOrDefaultAsync(x => x.Id == emailId, ct);

            if(email == null)
            {
                throw new InvalidOperationException($"Email message with ID {emailId} not found.");
            }

            foreach(var attachment in attachments)
            {
                attachment.EmailMessageId = emailId;
                _context.EmailAttachments.Add(attachment);
            }

            await _context.SaveChangesAsync(ct);
        }

        public async Task<int> BulkMarkDeadLetterAsync(DateTime nowUtc, CancellationToken ct)
        {
            var affected = await _context.EmailMessages.Where(x => x.Status != EmailStatusEnum.Sent &&
                                                                  x.Status != EmailStatusEnum.Failed &&
                                                                  x.Attempts >= x.MaxAttempts)
                                                       .ExecuteUpdateAsync(x => x.SetProperty(em => em.Status, EmailStatusEnum.Failed)
                                                                                .SetProperty(em => em.ScheduleAt, (DateTime?)null)
                                                                                .SetProperty(em => em.LastErrorMessage, x => x.LastErrorMessage ?? "Exceeded max attempts"), ct);
            return affected;
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

        public async Task<EmailMessage?> FindByDedupKeyAsync(string dedupKey, CancellationToken ct)
        {
            var check = await _context.EmailMessages.FirstOrDefaultAsync(x => x.DeduplicationKey == dedupKey && x.Status != EmailStatusEnum.Failed, ct);
            return check;
        }

        public async Task<EmailMessage?> FindByMessageIdAsync(Guid messageId, CancellationToken ct)
        {
            var check = await _context.EmailMessages.FirstOrDefaultAsync(x => x.MessageId == messageId, ct);
            return check;
        }

        public Task<List<EmailMessage>> TakeQueuedAsync(int take, DateTime utcNow, CancellationToken ct)
            => _context.EmailMessages
                  .Where(x => x.Status == EmailStatusEnum.Queued &&
                              (x.ScheduleAt == null || x.ScheduleAt <= utcNow))
                  .OrderByDescending(x => x.Priority).ThenBy(x => x.CreationTime)
                  .Take(take).ToListAsync(ct);

        public async Task<bool> TryMarkSendingAsync(int emailId, CancellationToken ct)
        {
            var nowUtc = DateTime.UtcNow;

            var affected = await _context.EmailMessages
                .Where(x =>
                    x.Id == emailId &&
                    x.Status == EmailStatusEnum.Queued &&
                    (x.ScheduleAt == null || x.ScheduleAt <= nowUtc) &&
                    x.Attempts < x.MaxAttempts)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.Status, EmailStatusEnum.Sending)
                    .SetProperty(x => x.LastErrorMessage, (string?)null),
                    ct);

            return affected == 1;
        }

        public async Task UpdateAsync(EmailMessage emailMessage)
        {
            _context.EmailMessages.Update(emailMessage);
            await _context.SaveChangesAsync();
        }
    }
}
