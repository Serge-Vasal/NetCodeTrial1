using ENet;
using Microsoft.Extensions.Logging;
using NetCodeTrial1.Server.Realtime.Contracts;
using NetCodeTrial1.Server.Realtime.Contracts.Channels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetCodeTrial1.Server.Realtime.Network.ENet
{
    public class ENetServer : INetworkServer
    {
        private readonly IDeserializationChannel<EnetNetworkMessage> deserializationChannel;
        private readonly ILogger<ENetServer> logger;

        private Host server;
        private CancellationTokenSource cancellationTokenSource;

        public ENetServer(
            IDeserializationChannel<EnetNetworkMessage> deserializationChannel,
            ILogger<ENetServer> logger)
        {
            this.deserializationChannel = deserializationChannel;
            this.logger = logger;
        }

        public Task Start(CancellationToken cancellationToken)
        {
            cancellationTokenSource = new CancellationTokenSource();
            server = new Host();
            Address address = default;
            address.Port = 40002;
            server.Create(address, 10, 4);

            Task.Factory.StartNew(() => RunLoop(server, cancellationTokenSource.Token), TaskCreationOptions.LongRunning);

            logger.LogInformation("Enet server started.");
            return Task.CompletedTask;
        }

        private void RunLoop(Host server, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    server.Service(15, out Event netEvent);

                    Process(ref netEvent);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        private void Process(ref Event networkEvent)
        {
            switch (networkEvent.Type)
            {
                case EventType.None:
                    break;

                case EventType.Connect:
                    logger.LogInformation("Client connected - ID: {peerId}, IP: {peerIP}", networkEvent.Peer.ID, networkEvent.Peer.IP);
                    Console.WriteLine("Connect Event received from client - ID: " + networkEvent.Peer.ID + ", IP: " + networkEvent.Peer.IP);
                    break;

                case EventType.Disconnect:
                    logger.LogInformation("Client disconnected - ID: {peerId}, IP: {peerIP}", networkEvent.Peer.ID, networkEvent.Peer.IP);
                    Console.WriteLine("Disconnect Event received from client - ID: " + networkEvent.Peer.ID + ", IP: " + networkEvent.Peer.IP);
                    break;

                case EventType.Timeout:
                    logger.LogInformation("Client timeout - ID: {peerId}, IP: {peerIP}", networkEvent.Peer.ID, networkEvent.Peer.IP);
                    Console.WriteLine("Client timeout - ID: " + networkEvent.Peer.ID + ", IP: " + networkEvent.Peer.IP);
                    break;

                case EventType.Receive:
                    Console.WriteLine("Packet received from - ID: " + networkEvent.Peer.ID + ", IP: " + networkEvent.Peer.IP + ", Channel ID: " + networkEvent.ChannelID + ", Data length: " + networkEvent.Packet.Length);

                    var receivedMessage = new EnetNetworkMessage(networkEvent.Packet, networkEvent.Packet.Length);
                    WriteToDeserializationChannel(in receivedMessage);
                    break;

                default:
                    throw new NotImplementedException($"The event type {networkEvent.Type} is not supported.");
            }
        }

        public Task Stop(CancellationToken cancellationToken)
        {
            cancellationTokenSource?.Cancel();

            server?.Dispose();
            server = null;

            return Task.CompletedTask;
        }

        private void WriteToDeserializationChannel(in EnetNetworkMessage message)
        {
            if (!deserializationChannel.TryWrite(in message))
            {
                logger.LogError("Message can't be written to the channel.");
            }
        }
    }
}
