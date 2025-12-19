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
    public class BounceLogRepository : IBounceLogRepository
    {
        private readonly ApplicationDbContext _context;
        public BounceLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(BounceLog bounceLog)
        {
            _context.BounceLogs.Add(bounceLog); 
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.BounceLogs.FirstOrDefaultAsync(x => x.Id == id);
            if(entity == null)
            {
                return false;
            }
            _context.BounceLogs.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateAsync(BounceLog bounceLog)
        {
            _context.BounceLogs.Update(bounceLog);
            await _context.SaveChangesAsync();
        }
    }
}
