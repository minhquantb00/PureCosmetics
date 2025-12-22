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
    /// <summary>
    /// Class DI for Infrastructure
    /// User created: QuanTM
    /// Created date: 2025/12/22
    /// Last modified date: 2025/12/22
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Method add infrastructure about create DI
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            /// <summary>
            /// DI for user repository
            /// </summary>
            services.AddScoped<IUserRepository, UserRepository>();

            /// <summary>
            /// DI for db context
            /// </summary>
            services.AddScoped<IDbContext, ApplicationDbContext>();

            /// <summary>
            /// DI for refresh token repository
            /// </summary>
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            /// <summary>
            /// DI for address repository
            /// </summary>
            services.AddScoped<IAddressRepository, AddressRepository>();

            /// <summary>
            /// DI for role repository
            /// </summary>
            services.AddScoped<IRoleRepository, RoleRepository>();
            return services;
        }
    }
}
