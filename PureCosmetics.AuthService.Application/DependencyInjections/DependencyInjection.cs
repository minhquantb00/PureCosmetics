using Microsoft.Extensions.DependencyInjection;
using PureCosmetics.AuthService.Application.ServiceContracts;
using PureCosmetics.AuthService.Application.ServiceImplements;
using PureCosmetics.AuthService.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.AuthService.Application.DependencyInjections
{
    /// <summary>
    /// Dependency injection for application
    /// User create: QuanTM
    /// Created date: 2025/12/13
    /// Last modified date: 2025/12/13
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Add application
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            /// <summary>
            /// Inject user service
            /// </summary>
            services.AddScoped<IUserService, UserService>();

            /// <summary>
            /// Interface address service
            /// </summary>
            services.AddScoped<IAddressService, AddressService>();

            /// <summary>
            /// Return services
            /// </summary>
            return services;
        }
    }
}
