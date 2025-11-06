using PureCosmetics.AuthService.Domain.Entities;
using PureCosmetics.AuthService.Domain.RepositoryContracts;
using PureCosmetics.AuthService.Infrastructure.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Infrastructure.RepositoryImplements
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;
        protected IDbContext _dbContext;
        public RefreshTokenRepository(IDbContext dbContext, ApplicationDbContext context)
        {
            _dbContext = dbContext;
            _context = context;
        }
        public async Task CreateAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
        }
    }
}
