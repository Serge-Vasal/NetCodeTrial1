using System;
using UnityEngine;
using UnityEngine.UI;

namespace NetCodeTrial1.Client.UI.Views
{
    public class LobbyView : MonoBehaviour
    {
        [SerializeField]
        private Button connectButton;

        [SerializeField]
        private Text debugText;

        public event Action ConnectButtonClickedEvent;

        public string DebugText
        {
            get => debugText.text;
            set => debugText.text = value;
        }

        private void OnEnable()
        {
            connectButton.onClick.AddListener(() => ConnectButtonClickedEvent?.Invoke());
        }
    }
}