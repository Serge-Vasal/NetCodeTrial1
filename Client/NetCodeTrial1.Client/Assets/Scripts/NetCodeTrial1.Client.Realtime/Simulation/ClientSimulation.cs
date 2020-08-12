using NetCodeTrial1.Client.Realtime.Service;
using NetCodeTrial1.Common.Realtime.Data.Commands;

namespace NetCodeTrial1.Client.Realtime.Simulation
{
    public class ClientSimulation : IClientSimulation
    {
        private readonly INetworkService networkService;

        public ClientSimulation(INetworkService networkService)
        {
            this.networkService = networkService;
        }

        public void AddCommand(IGameCommand gameCommand)
        {
            networkService.Send(new SimulationCommand(gameCommand));
        }

        public void SendCommand(IGameCommand gameCommand)
        {
            networkService.Send(new SimulationCommand(gameCommand));
        }
    }
}