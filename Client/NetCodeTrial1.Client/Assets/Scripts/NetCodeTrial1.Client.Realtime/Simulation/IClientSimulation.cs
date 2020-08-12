using NetCodeTrial1.Common.Realtime.Data.Commands;

namespace NetCodeTrial1.Client.Realtime.Simulation
{
    public interface IClientSimulation
    {
        void AddCommand(IGameCommand gameCommand);

        void SendCommand(IGameCommand gameCommand);
    }
}