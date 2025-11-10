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
    public class InboxMessageRepository : IInboxMessageRepository
    {
        private readonly ApplicationDbContext _context;
        public InboxMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(InboxMessage inboxMessage)
        {
            _context.InboxMessages.Add(inboxMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.InboxMessages.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                _context.InboxMessages.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task UpdateAsync(InboxMessage inboxMessage)
        {
            _context.InboxMessages.Update(inboxMessage);
            await _context.SaveChangesAsync();
        }
    }
}
