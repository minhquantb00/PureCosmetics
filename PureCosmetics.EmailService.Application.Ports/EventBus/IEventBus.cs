using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Application.Ports.EventBus
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T evt, CancellationToken ct);
    }
}
