using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCodeTrial1.Server.Realtime.Contracts;
using NetCodeTrial1.Server.Realtime.Network.ENet;
using NetCodeTrial1.Server.Realtime.Runtime.Configuration;
using NetCodeTrial1.Server.Realtime.Runtime.Runtime;

namespace NetCodeTrial1.Server.Realtime.Application
{
    public sealed class RealtimeServer
    {
        public static IHostBuilder Create(string basePath, string[] args)
        {
            return new HostBuilder()
                .UseEnvironment(Environments.Development)
                .UseContentRoot(basePath)
                .ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
                {
                    var env = hostBuilderContext.HostingEnvironment;

                    configurationBuilder
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile("config/realtime.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);
                })
                .ConfigureServices((hostBuilderContext, services) =>
                {
                    services.AddSingleton<INetworkServer, ENetServer>();
                    services.AddSingleton<IServerAddressProvider, ServerAddressProvider>();
                    services.AddSingleton<INetworkConfiguration, NetworkConfiguration>();

                    services.AddHostedService<ENetServerHostedService>();
                });
        }
    }
}
