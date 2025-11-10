using MassTransit;
using PureCosmetics.EmailService.Application.Ports.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Infrastructure.Messaging
{
    public sealed class MassTransitEventBus : IEventBus
    {
        private readonly IPublishEndpoint _bus;
        public MassTransitEventBus(IPublishEndpoint bus) => _bus = bus;
        public Task PublishAsync<T>(T evt, CancellationToken ct) => _bus.Publish(evt, ct);
    }
}
