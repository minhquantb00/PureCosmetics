using PureCosmetics.AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Domain.RepositoryContracts
{
    public interface IRefreshTokenRepository
    {
        Task CreateAsync(RefreshToken refreshToken);
    }
}
