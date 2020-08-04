using NetCodeTrial1.Client.Core;
using NetCodeTrial1.Client.UI.Core;
using NetCodeTrial1.Client.UI.Presenters;
using NetCodeTrial1.Client.UI.Views;
using UnityEngine;

namespace NetCodeTrial1.Client.UI.Screens
{
    public class LobbyScreen : BaseScreen
    {
        [SerializeField]
        private LobbyView lobbyView = null;

        private LobbyScreenPresenter lobbyScreenPresenter;

        public void Setup(Main main)
        {
            lobbyScreenPresenter = new LobbyScreenPresenter(main, lobbyView);
            AddDisposablePresenter(lobbyScreenPresenter);
        }
    }
}