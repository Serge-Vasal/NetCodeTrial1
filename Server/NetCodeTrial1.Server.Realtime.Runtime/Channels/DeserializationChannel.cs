using NetCodeTrial1.Server.Realtime.Contracts.Channels;
using NetCodeTrial1.Server.Realtime.Contracts.Messages;

namespace NetCodeTrial1.Server.Realtime.Runtime.Channels
{
    public class DeserializationChannel<TMessage> : MessageChannel<TMessage>, IDeserializationChannel<TMessage>
        where TMessage : INetworkMessage
    {
        public DeserializationChannel(IChannelHandler channelHandler)
            : base(channelHandler, singleReader: true, singleWriter: true)
        {
        }
    }
}
