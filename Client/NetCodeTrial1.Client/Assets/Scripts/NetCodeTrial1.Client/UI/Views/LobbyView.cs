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

        [SerializeField]
        private InputField inputField;

        public event Action ConnectButtonClickedEvent;

        public event Action<string> InputEditEndedEvent;

        public string DebugText
        {
            get => debugText.text;
            set => debugText.text = value;
        }

        private void OnEnable()
        {
            connectButton.onClick.AddListener(() => ConnectButtonClickedEvent?.Invoke());

            inputField.onEndEdit.AddListener((text) => InputEditEndedEvent?.Invoke(text));
        }
    }
}