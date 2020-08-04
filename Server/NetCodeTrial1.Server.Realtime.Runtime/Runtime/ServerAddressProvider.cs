using Microsoft.Extensions.Logging;
using NetCodeTrial1.Server.Realtime.Runtime.Configuration;
using System.Net;
using System.Threading.Tasks;

namespace NetCodeTrial1.Server.Realtime.Runtime.Runtime
{
    public class ServerAddressProvider : IServerAddressProvider
    {
        private readonly INetworkConfiguration networkConfiguration;
        private readonly ILogger<ServerAddressProvider> logger;

        private IPEndPoint serverAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerAddressProvider"/> class.
        /// </summary>
        /// <param name="networkConfiguration">The network configuration.</param>
        /// <param name="logger">The logger.</param>
        public ServerAddressProvider(INetworkConfiguration networkConfiguration, ILogger<ServerAddressProvider> logger)
        {
            this.networkConfiguration = networkConfiguration;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the server address.
        /// </summary>
        /// <returns></returns>
        public async Task<IPEndPoint> GetServerAddress()
        {
            if (serverAddress == null)
            {
                serverAddress = await networkConfiguration.GetPublicIPAddress();
                logger.LogInformation("Realtime server public address {serverAddress} obtained", serverAddress);
            }

            return serverAddress;
        }
    }
}
