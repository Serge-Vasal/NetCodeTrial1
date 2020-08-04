using Microsoft.Extensions.Hosting;
using NetCodeTrial1.Server.Realtime.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;
using ENetLib = ENet.Library;

namespace NetCodeTrial1.Server.Realtime.Network.ENet
{
    public class ENetServerHostedService : IHostedService, IDisposable
    {
        private readonly INetworkServer networkServer;

        public ENetServerHostedService(INetworkServer networkServer)
        {
            ENetLib.Initialize();
            this.networkServer = networkServer;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await networkServer.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await networkServer.Stop(cancellationToken);
        }

        public void Dispose()
        {
            ENetLib.Deinitialize();
        }
    }
}
