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

        public async Task UpdateAsync(OutboxMessage outboxMessage)
        {
            _context.OutboxMessages.Update(outboxMessage);
            await _context.SaveChangesAsync();
        }
    }
}
