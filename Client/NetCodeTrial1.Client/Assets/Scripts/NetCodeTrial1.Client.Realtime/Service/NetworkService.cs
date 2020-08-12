using NetCodeTrial1.Client.Realtime.Connection;
using NetCodeTrial1.Common.Realtime.Data.Commands;
using NetCodeTrial1.Common.Realtime.Serialization;

namespace NetCodeTrial1.Client.Realtime.Service
{
    public class NetworkService : INetworkService
    {
		private readonly ICommunication communication;

		private SimulationCommandSerializer commandSerializer = new SimulationCommandSerializer();

		public NetworkService(ICommunication communication)
		{
			this.communication = communication;
		}

		public void QueueCommand(SimulationCommand command)
		{
			commandSerializer.AddCommand(command);
		}

		public void Send()
		{
			communication.ServiceOnce();
		}

		public void Send(SimulationCommand command)
		{
			var data = commandSerializer.Serialize(command);
			communication.SendReliable(data, data.Length);

			communication.ServiceOnce();
		}
	}
}