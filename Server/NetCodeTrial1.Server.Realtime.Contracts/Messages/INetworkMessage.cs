using System;

namespace NetCodeTrial1.Server.Realtime.Contracts.Messages
{
    public interface INetworkMessage
    {
        int Length { get; }

        Span<byte> Span { get; }

        void Dispose();
    }
}
