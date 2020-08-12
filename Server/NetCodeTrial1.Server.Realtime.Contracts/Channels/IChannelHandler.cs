using System;
using System.Threading;
using System.Threading.Channels;

namespace NetCodeTrial1.Server.Realtime.Contracts.Channels
{
    public interface IChannelHandler
    {
        void StartProcessingMessages<TMessage>(Channel<TMessage> channel, Action<TMessage> processor, CancellationToken cancellationToken = default);

        void StopProcessingMessages<TMessage>(Channel<TMessage> channel);
    }
}
