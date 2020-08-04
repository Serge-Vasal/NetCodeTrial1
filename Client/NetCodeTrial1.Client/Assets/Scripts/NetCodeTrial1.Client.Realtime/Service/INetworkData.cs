using NetCodeTrial1.Common.Realtime.Service;
using System;

namespace NetCodeTrial1.Client.Realtime.Service
{
    public interface INetworkData
    {
        Span<byte> Span { get; }

        NetworkDataCode Code { get; }

        int Length { get; }

        void Dispose();
    }
}