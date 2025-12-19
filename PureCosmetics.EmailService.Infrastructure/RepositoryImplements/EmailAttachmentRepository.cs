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
    public class EmailAttachmentRepository : IEmailAttachmentRepository
    {
        private readonly ApplicationDbContext _context;
        public EmailAttachmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(EmailAttachment emailAttachment)
        {
            _context.EmailAttachments.Add(emailAttachment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.EmailAttachments.FirstOrDefaultAsync(e => e.Id == id);
            if (entity != null)
            {
                _context.EmailAttachments.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task UpdateAsync(EmailAttachment emailAttachment)
        {
            _context.EmailAttachments.Update(emailAttachment);
            await _context.SaveChangesAsync();
        }
    }
}
