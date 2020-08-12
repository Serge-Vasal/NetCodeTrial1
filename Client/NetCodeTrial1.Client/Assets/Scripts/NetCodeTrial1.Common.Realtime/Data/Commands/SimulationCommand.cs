namespace NetCodeTrial1.Common.Realtime.Data.Commands
{
    public sealed class SimulationCommand
    {
        public IGameCommand GameCommand { get; }

        public SimulationCommand(IGameCommand gameCommand)
        {
            GameCommand = gameCommand;
        }
    }
}
