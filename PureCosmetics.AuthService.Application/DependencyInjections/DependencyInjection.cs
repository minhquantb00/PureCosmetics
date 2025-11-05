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
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
