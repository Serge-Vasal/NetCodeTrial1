using NetCodeTrial1.Common.Realtime.Serialization;
using NetCodeTrial1.Server.Realtime.Contracts;
using NetCodeTrial1.Server.Realtime.Contracts.Channels;
using NetCodeTrial1.Server.Realtime.Contracts.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace NetCodeTrial1.Server.Realtime.Runtime
{
    public class DeserializationService<TNetworkMessage> : IDeserializationService
        where TNetworkMessage : INetworkMessage
    {
        private readonly IDeserializationChannel<TNetworkMessage> deserializationChannel;
        private readonly SimulationCommandSerializer commandSerializer;

        private CancellationTokenSource cancellationTokenSource;

        public DeserializationService(
            IDeserializationChannel<TNetworkMessage> deserializationChannel,
            SimulationCommandSerializer commandSerializer)
        {
            this.deserializationChannel = deserializationChannel;
            this.commandSerializer = commandSerializer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            cancellationTokenSource = new CancellationTokenSource();
            deserializationChannel.StartProcessing(message => ProcessMessage(in message), cancellationTokenSource.Token);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            deserializationChannel.StopProcessing();

            cancellationTokenSource?.Cancel();

            return Task.CompletedTask;
        }

        private void ProcessMessage(in TNetworkMessage message)
        {
            var commands = commandSerializer.Deserialize(message.Span);
        }
    }
}
