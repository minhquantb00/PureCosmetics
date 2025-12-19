using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Grpc.Net.ClientFactory;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.ClientFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.GrpcClient
{
    /// <summary>
    /// GRPC client resolver extensions for registering gRPC clients with load balancing and retry policies.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    public static partial class GrpcClientResolver
    {
        /// <summary>
        /// Registers gRPC client load balancing support.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterGrpcClientLoadBalancing(this IServiceCollection services)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            return services;
        }

        /// <summary>
        /// Configures gRPC client options with load balancing and retry policies.
        /// </summary>
        /// <param name="grpcClientFactoryOptions"></param>
        /// <param name="address"></param>
        /// <param name="serviceProvider"></param>
        private static void ConfigGrpcClientOptions(GrpcClientFactoryOptions grpcClientFactoryOptions, string address,
        IServiceProvider serviceProvider)
        {
            SocketsHttpHandler socketsHttpHandler = new SocketsHttpHandler()
            {
                PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                EnableMultipleHttp2Connections = true,
                MaxConnectionsPerServer = int.MaxValue,
            };
            var methodConfig = new MethodConfig
            {
                Names = { MethodName.Default },
                RetryPolicy = new RetryPolicy
                {
                    MaxAttempts = 5,
                    InitialBackoff = TimeSpan.FromSeconds(1),
                    MaxBackoff = TimeSpan.FromSeconds(5),
                    BackoffMultiplier = 1.5,
                    RetryableStatusCodes = { StatusCode.Unavailable }
                }
            };
            grpcClientFactoryOptions.Address = new Uri(address);
            grpcClientFactoryOptions.ChannelOptionsActions.Add(o =>
            {
                o.HttpHandler = socketsHttpHandler;
                o.MaxReceiveMessageSize = int.MaxValue;
                o.MaxSendMessageSize = int.MaxValue;
                o.Credentials = ChannelCredentials.Insecure;
                o.ServiceProvider = serviceProvider;
                o.ServiceConfig = new ServiceConfig
                {
                    LoadBalancingConfigs = { new RoundRobinConfig() },
                    MethodConfigs = { methodConfig }
                };
            });
        }

        /// <summary>
        /// Registers a gRPC client with load balancing support.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterGrpcClientLoadBalancing<T>(this IServiceCollection services, string? url)
        where T : class
        {
            if (url is not { Length: > 0 })
            {
                return services;
            }

            services.AddCodeFirstGrpcClient<T>((provider, options) => { ConfigGrpcClientOptions(options, url, provider); })
                .AddInterceptor(provider =>
                {
                    LoggerInterceptor loggerInterceptor = provider.GetRequiredService<LoggerInterceptor>();
                    loggerInterceptor.Host = url;
                    return loggerInterceptor;
                });
            return services;
        }

        /// <summary>
        /// Registers a gRPC client with load balancing support and a specified name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterGrpcClientLoadBalancing<T>(this IServiceCollection services, string? url,
        string? name)
        where T : class
        {
            if (url is not { Length: > 0 })
            {
                return services;
            }

            if (name?.Length > 0)
            {
                services.AddCodeFirstGrpcClient<T>(name,
                        (provider, options) => { ConfigGrpcClientOptions(options, url, provider); })
                    .AddInterceptor(provider =>
                    {
                        LoggerInterceptor loggerInterceptor = provider.GetRequiredService<LoggerInterceptor>();
                        loggerInterceptor.Host = url;
                        return loggerInterceptor;
                    });
            }
            else
            {
                RegisterGrpcClientLoadBalancing<T>(services, url);
            }

            return services;
        }
    }
}
