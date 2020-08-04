using System;

namespace NetCodeTrial1.Client.Realtime.Connection
{
    public interface IGameplayConnection : IConnection, ICommunication
    {
        event Action OnDisconnected;

        event Action OnDisconnectedByServer;
    }
}