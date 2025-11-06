using Microsoft.Extensions.DependencyInjection;
using PureCosmetics.AuthService.Application.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.GrpcClient
{
    public static partial class GrpcClientResolver
    {
        public static IServiceCollection UserManagerRegisterGrpcClient(this IServiceCollection services)
        {
            services.RegisterGrpcClientLoadBalancing<IUserService>();
            return services;
        }
    }
}
