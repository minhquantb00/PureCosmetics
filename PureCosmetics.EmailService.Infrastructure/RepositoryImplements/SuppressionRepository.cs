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
    public class SuppressionRepository : ISuppressionRepository
    {
        private readonly ApplicationDbContext _context;
        public SuppressionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Suppression suppression)
        {
            _context.Subpressions.Add(suppression);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity =  await _context.Subpressions.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                _context.Subpressions.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Task<bool> IsSuppressedAsync(string email, CancellationToken ct)
        => _context.Subpressions.AnyAsync(s => s.EmailAddress == email &&
            (s.ExpiresAt == null || s.ExpiresAt > DateTime.UtcNow), ct);

        public async Task UpdateAsync(Suppression suppression)
        {
            _context.Subpressions.Update(suppression);
            await _context.SaveChangesAsync();
        }
    }
}
