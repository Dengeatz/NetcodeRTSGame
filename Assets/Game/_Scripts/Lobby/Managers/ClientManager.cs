using System;
using TMPro;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using UnityEngine;

namespace RTS
{
    public class ClientManager : MonoBehaviour
    {
        [SerializeField] private Canvas clientCanvas;
        [SerializeField] private TMP_Text errorText;
        [SerializeField] private TMP_InputField inputField;

        public Action ClientExit;
        public Action ClientConnected;

        public void ShowScreen()
        {
            clientCanvas.gameObject.SetActive(true);
        }

        public void HideScreen()
        {
            clientCanvas.gameObject.SetActive(false);
        }

        public void OnHideButton()
        {
            ClientExit?.Invoke();
        }

        public void DisconnectFromHost()
        {
            NetworkManager.Singleton.Shutdown();
        }

        public void OnConnectButton()
        {
            TryConnectToServer();
        }

        private void TryConnectToServer()
        {
            try
            {
                ConnectToServer();
                ClientConnected?.Invoke();
            }
            catch (Exception e)
            {
                errorText.text = e.Message;
            }
        }

        private void ConnectToServer()
        {
            if (inputField.text == null || inputField.text == "")
            {
                throw new Exception("IpField is null!");
            }

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
                inputField.text,
                (ushort)7777
            );

            
            bool isSuccesful = NetworkManager.Singleton.StartClient();

            if (!isSuccesful)
                throw new Exception("Connection error");
        }
    }
}
