using Microsoft.Extensions.DependencyInjection;
using PureCosmetics.AuthService.Domain.RepositoryContracts;
using PureCosmetics.AuthService.Infrastructure.ORM;
using PureCosmetics.AuthService.Infrastructure.RepositoryImplements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Infrastructure.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDbContext, ApplicationDbContext>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            return services;
        }
    }
}
