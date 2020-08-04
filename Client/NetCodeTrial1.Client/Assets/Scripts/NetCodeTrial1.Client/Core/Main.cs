using NetCodeTrial1.Client.Network.ENet;
using NetCodeTrial1.Client.Realtime.Connection;
using NetCodeTrial1.Client.Realtime.Service;
using NetCodeTrial1.Client.UI.Core;
using NetCodeTrial1.Client.UI.Screens;
using System;
using UnityEngine;

namespace NetCodeTrial1.Client.Core
{
    public class Main : MonoBehaviour
    {
        [SerializeField]
        private ScreensController screensController;

        private INetworkService networkService;

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
                ServerHostName = "18.185.139.165",
                ServerPort = 40002
            });

            networkService = new NetworkService(connection);

            connection.Connect();
        }
    }
}