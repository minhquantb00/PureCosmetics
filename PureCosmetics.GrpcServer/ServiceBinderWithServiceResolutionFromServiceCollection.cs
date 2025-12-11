using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.GrpcServer
{
    /// <summary>
    /// Service binder that resolves service types from the provided IServiceCollection.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    /// <param name="services"></param>
    public class ServiceBinderWithServiceResolutionFromServiceCollection(IServiceCollection services) : ServiceBinder
    {
        /// <summary>
        /// Gets metadata for the specified method, resolving the service type from the IServiceCollection if it's an interface.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="contractType"></param>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public override IList<object> GetMetadata(MethodInfo method, Type contractType, Type serviceType)
        {
            var resolvedServiceType = serviceType;
            if (serviceType.IsInterface)
                resolvedServiceType = services.SingleOrDefault(x => x.ServiceType == serviceType)?.ImplementationType ??
                                      serviceType;

            return base.GetMetadata(method, contractType, resolvedServiceType);
        }
    }
}
