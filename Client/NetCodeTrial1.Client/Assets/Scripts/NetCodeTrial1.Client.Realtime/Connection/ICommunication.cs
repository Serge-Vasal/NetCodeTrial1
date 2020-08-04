using NetCodeTrial1.Client.Realtime.Service;

namespace NetCodeTrial1.Client.Realtime.Connection
{
    public interface ICommunication
    {
        void SendUnreliable(byte[] data, int length);

        void SendReliable(byte[] data, int length);

        void ServiceAll();

        void ServiceOnce();

        bool HasData();

        INetworkData GetData();
    }
}
