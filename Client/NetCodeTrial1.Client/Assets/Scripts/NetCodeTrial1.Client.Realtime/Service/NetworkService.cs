using NetCodeTrial1.Client.Realtime.Connection;

namespace NetCodeTrial1.Client.Realtime.Service
{
    public class NetworkService : INetworkService
    {
		private readonly ICommunication communication;

		public NetworkService(ICommunication communication)
		{
			this.communication = communication;
		}

		public void Send()
		{
			communication.ServiceOnce();
		}
	}
}