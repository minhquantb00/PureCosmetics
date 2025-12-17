using Microsoft.Extensions.DependencyInjection;
using PureCosmetics.AuthService.Application.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.GrpcClient
{
    /// <summary>
    /// Grpc client resolver extensions for registering UserManager gRPC client.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    public static partial class GrpcClientResolver
    {
        /// <summary>
        /// UserManager gRPC client registration with load balancing.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection UserManagerRegisterGrpcClient(this IServiceCollection services)
        {
            //services.RegisterGrpcClientLoadBalancing<IUserService>();
            return services;
        }
    }
}
