using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetCodeTrial1.Server.Common;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCodeTrial1.Server.Realtime.Runtime.Configuration
{
    public class NetworkConfiguration : INetworkConfiguration
    {
        private readonly IConfiguration configuration;
        private readonly IPUtils ipUtils;

        public NetworkConfiguration(IConfiguration configuration, ILogger<NetworkConfiguration> logger)
        {
            this.configuration = configuration;
            this.ipUtils = new IPUtils(new HttpClient(), logger);
        }

        public async Task<IPEndPoint> GetPublicIPAddress()
        {
            var configAddress = configuration.GetValue<string>("Realtime:PublicIPAddress");
            var configPort = configuration.GetValue<int>("EnetServer:Port");

            if (string.IsNullOrEmpty(configAddress))
            {
                var discoveredAddress = await ipUtils.GetPublicAddress();
                return new IPEndPoint(discoveredAddress, configPort);
            }

            return new IPEndPoint(IPAddress.Parse(configAddress), configPort);
        }
    }
}
