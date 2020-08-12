using NetCodeTrial1.Server.Realtime.Contracts.Messages;

namespace NetCodeTrial1.Server.Realtime.Contracts.Channels
{
    public interface IDeserializationChannel<TMessage> : IMessageChannel<TMessage>
        where TMessage : INetworkMessage
    {
    }
}
