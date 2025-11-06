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
    public class ServiceBinderWithServiceResolutionFromServiceCollection(IServiceCollection services) : ServiceBinder
    {
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
