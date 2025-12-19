using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProtoBuf.Grpc.Configuration;
using ProtoBuf.Grpc.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.GrpcServer
{
    /// <summary>
    /// Services extensions for configuring Code-First gRPC services.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    public static class ServicesExtensions
    {
        /// <summary>
        /// Configures Code-First gRPC services with custom settings.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigCodeFirstGrpc(this IServiceCollection services)
        {
            services.AddCodeFirstGrpc(config =>
            {
                config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.NoCompression;
                config.MaxReceiveMessageSize = int.MaxValue;
                config.MaxSendMessageSize = int.MaxValue;
            });
            services.TryAddSingleton(
                BinderConfiguration.Create(
                    binder: new ServiceBinderWithServiceResolutionFromServiceCollection(services)));
            services.AddCodeFirstGrpcReflection();
            return services;
        }
    }
}
