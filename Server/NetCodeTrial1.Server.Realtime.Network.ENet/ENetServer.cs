using ENet;
using NetCodeTrial1.Server.Realtime.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetCodeTrial1.Server.Realtime.Network.ENet
{
    public class ENetServer : INetworkServer
    {
        private Host server;
        private Address address;

        private CancellationTokenSource cancellationTokenSource;

        public ENetServer()
        {
        }

        public Task Start(CancellationToken cancellationToken)
        {
            cancellationTokenSource = new CancellationTokenSource();

            server = new Host();

            address = default;
            address.Port = 40002;
            server.Create(address, 10, 4);

            Task.Factory.StartNew(() => RunLoop(server, cancellationTokenSource.Token), TaskCreationOptions.LongRunning);

            return Task.CompletedTask;
        }

        private void RunLoop(Host server, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    Event netEvent;

                    while (!Console.KeyAvailable)
                    {
                        bool polled = false;

                        while (!polled)
                        {
                            if (server.CheckEvents(out netEvent) <= 0)
                            {
                                if (server.Service(15, out netEvent) <= 0)
                                    break;

                                polled = true;
                            }

                            switch (netEvent.Type)
                            {
                                case EventType.None:
                                    break;

                                case EventType.Connect:
                                    Console.WriteLine("Connect Event received from client - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                                    break;

                                case EventType.Disconnect:
                                    Console.WriteLine("Disconnect Event received from client - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                                    break;

                                case EventType.Timeout:
                                    Console.WriteLine("Client timeout - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                                    break;

                                case EventType.Receive:
                                    Console.WriteLine("Packet received from - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP + ", Channel ID: " + netEvent.ChannelID + ", Data length: " + netEvent.Packet.Length);
                                    netEvent.Packet.Dispose();
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        public Task Stop(CancellationToken cancellationToken)
        {
            cancellationTokenSource?.Cancel();

            server?.Dispose();
            server = null;

            return Task.CompletedTask;
        }
    }
}
