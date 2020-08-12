using NetCodeTrial1.Client.Network.ENet;
using NetCodeTrial1.Client.Realtime.Connection;
using NetCodeTrial1.Client.Realtime.Service;
using NetCodeTrial1.Client.Realtime.Simulation;
using NetCodeTrial1.Client.UI.Core;
using NetCodeTrial1.Client.UI.Screens;
using NetCodeTrial1.Common.Realtime.Data.Commands;
using UnityEngine;

namespace NetCodeTrial1.Client.Core
{
    public class Main : MonoBehaviour
    {
        [SerializeField]
        private ScreensController screensController;

        private INetworkService networkService;

        public IClientSimulation ClientSimulation { get; private set; }

        private void Start()
        {
            var lobbyScreen = screensController.ShowScreen<LobbyScreen>();
            lobbyScreen.Setup(this);
        }

        private void Update()
        {
            networkService?.Send();
        }

        public void ConnectToRoom()
        {
            IGameplayConnection connection = new ENetClient(new ENetClientSettings
            {
                //ServerHostName = "18.185.139.165",
                ServerHostName = "127.0.0.1",
                ServerPort = 40002
            });

            networkService = new NetworkService(connection);
            ClientSimulation = new ClientSimulation(networkService);

            connection.Connect();
        }

        public void AddCommand(IGameCommand gameCommand) => ClientSimulation.AddCommand(gameCommand);
    }
}