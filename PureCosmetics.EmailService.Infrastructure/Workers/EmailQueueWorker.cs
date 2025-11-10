using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.EmailService.Infrastructure.Workers
{
    public sealed class EmailQueueWorker : BackgroundService
    {
        //private readonly IProcessEmailQueue _processor;
        private readonly ILogger<EmailQueueWorker> _log;

        //public EmailQueueWorker(IProcessEmailQueue processor, ILogger<EmailQueueWorker> log)
        //    => (_processor, _log) = (processor, log);

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                try
                {
                    //var processed = await _processor.RunOnceAsync(ct);
                    //await Task.Delay(processed == 0 ? 1500 : 200, ct);
                }
                catch (Exception ex)
                {
                    _log.LogError(ex, "EmailQueueWorker error");
                    await Task.Delay(2000, ct);
                }
            }
        }
    }

}
