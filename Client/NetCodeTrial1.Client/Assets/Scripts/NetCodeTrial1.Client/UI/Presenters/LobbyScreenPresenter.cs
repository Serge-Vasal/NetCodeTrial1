using NetCodeTrial1.Client.Core;
using NetCodeTrial1.Client.UI.Core.Contracts;
using NetCodeTrial1.Client.UI.Views;
using System;
using UniRx;

namespace NetCodeTrial1.Client.UI.Presenters
{
    public class LobbyScreenPresenter : IDisposablePresenter
    {
        private readonly CompositeDisposable subscriptions = new CompositeDisposable();
        private readonly Main main;
        private readonly LobbyView lobbyView;

        public LobbyScreenPresenter(Main main, LobbyView lobbyView)
        {
            this.main = main;
            this.lobbyView = lobbyView;

            Observable.FromEvent(h => lobbyView.ConnectButtonClickedEvent += h, h => lobbyView.ConnectButtonClickedEvent -= h)
                .Subscribe(onNext => ConnectButtonEventHandler())
                .AddTo(subscriptions);
        }

        public void Dispose()
        {
            subscriptions.Dispose();
        }

        private void ConnectButtonEventHandler()
        {
            try
            {
                main.ConnectToRoom();
            }
            catch(Exception e)
            {
                lobbyView.DebugText = $"{e}, \n {e.Message}, \n {e.StackTrace}";
            }
        }
    }
}