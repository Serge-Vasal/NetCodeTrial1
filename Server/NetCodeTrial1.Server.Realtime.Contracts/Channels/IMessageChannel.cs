using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetCodeTrial1.Server.Realtime.Contracts.Channels
{
    public interface IMessageChannel<TMessage>
    {
        void StartProcessing(Action<TMessage> processor, CancellationToken cancellationToken = default);

        void StopProcessing();

        ValueTask WriteAsync(TMessage message, CancellationToken cancellationToken = default);

        bool TryWrite(in TMessage message);
    }
}
