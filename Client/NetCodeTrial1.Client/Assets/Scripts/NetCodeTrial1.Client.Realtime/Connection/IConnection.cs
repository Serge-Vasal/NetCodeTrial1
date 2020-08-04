namespace NetCodeTrial1.Client.Realtime.Connection
{
    public interface IConnection
    {
        ConnectionState ConnectionState { get; }

        void Connect();

        void Disconnect();

        void DisconnectNow();

        string ServerAddress { get; }
    }
}