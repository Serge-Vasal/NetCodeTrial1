using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCodeTrial1.Server.Realtime.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace NetCodeTrial1.Server.Realtime.Runtime.Runtime
{
    public class DeserializationHostedService : IHostedService
    {
        private readonly IDeserializationService deserializationService;
        private readonly ILogger<DeserializationHostedService> logger;

        public DeserializationHostedService(IDeserializationService deserializationService, ILogger<DeserializationHostedService> logger)
        {
            this.deserializationService = deserializationService;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await deserializationService.StartAsync(cancellationToken);
            logger.LogInformation("{service} started.", nameof(DeserializationHostedService));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await deserializationService.StopAsync(cancellationToken);
            logger.LogInformation("{service} stopped.", nameof(DeserializationHostedService));
        }
    }
}
