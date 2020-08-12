using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCodeTrial1.Common.Realtime.Serialization;
using NetCodeTrial1.Server.Realtime.Contracts;
using NetCodeTrial1.Server.Realtime.Contracts.Channels;
using NetCodeTrial1.Server.Realtime.Network.ENet;
using NetCodeTrial1.Server.Realtime.Runtime;
using NetCodeTrial1.Server.Realtime.Runtime.Channels;
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
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);
                })
                .ConfigureServices((hostBuilderContext, services) =>
                {
                    services.AddSingleton<IDeserializationChannel<EnetNetworkMessage>, DeserializationChannel<EnetNetworkMessage>>();
                    services.AddSingleton<IDeserializationService, DeserializationService<EnetNetworkMessage>>();

                    services.AddSingleton<INetworkServer, ENetServer>();
                    services.AddHostedService<ENetServerHostedService>();

                    services.AddHostedService<DeserializationHostedService>();

                    services.AddSingleton<IChannelHandler, ChannelHandler>();

                    services.AddTransient<SimulationCommandSerializer>();
                });
        }
    }
}
