using NetCodeTrial1.Common.Realtime.Data.Commands;

namespace NetCodeTrial1.Client.Realtime.Service
{
    public interface INetworkService
    {
        void QueueCommand(SimulationCommand command);

        void Send();

        void Send(SimulationCommand gameCommand);
    }
}