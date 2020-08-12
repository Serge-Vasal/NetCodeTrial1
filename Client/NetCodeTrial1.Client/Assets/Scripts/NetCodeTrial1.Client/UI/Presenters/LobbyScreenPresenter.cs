using NetCodeTrial1.Client.Core;
using NetCodeTrial1.Client.UI.Core.Contracts;
using NetCodeTrial1.Client.UI.Views;
using NetCodeTrial1.Common.Realtime.Data.Commands;
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

            Observable.FromEvent<string>(h => lobbyView.InputEditEndedEvent += h, h => lobbyView.InputEditEndedEvent -= h)
                .Subscribe(input => InputEditEndedEventHandler(input))
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

        private void InputEditEndedEventHandler(string input)
        {
            Byte result = Byte.Parse(input);
            var byteCommand = new ByteCommand(result);
            main.AddCommand(byteCommand);
        }
    }
}