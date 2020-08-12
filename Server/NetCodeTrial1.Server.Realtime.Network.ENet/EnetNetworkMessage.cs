using ENet;
using NetCodeTrial1.Server.Realtime.Contracts.Messages;
using System;

namespace NetCodeTrial1.Server.Realtime.Network.ENet
{
    public readonly struct EnetNetworkMessage : INetworkMessage
    {
        private readonly Packet? packet;

        public int Length { get; }

        public Span<byte> Span
        {
            get
            {
                unsafe
                {
                    var span = new Span<byte>(packet.GetValueOrDefault().Data.ToPointer(), Length);
                    return span;
                }
            }
        }

        public EnetNetworkMessage(Packet packet, int length)
        {
            this.packet = packet;
            Length = length;
        }

        public void Dispose()
        {
            packet.GetValueOrDefault().Dispose();
        }
    }
}
