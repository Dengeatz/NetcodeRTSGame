using System;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace RTS.Lobby.Manager
{
    public class LobbyManager : MonoBehaviour
    {
        [Inject] private HostManager hostManager;
        [Inject] private ClientManager clientManager;

        [SerializeField] private Canvas lobbyCanvas;

        private NetworkManager m_networkManager;

        private void Awake()
        {
            m_networkManager = NetworkManager.Singleton;
        }

        private void Start()
        {
            InstallEvents();
        }

        public void OnHostButton()
        {
            this.HideScreen();
            hostManager.ShowScreen();
            hostManager.StartHosting();

        }

        public void OnClientButton()
        {
            this.HideScreen();
            clientManager.ShowScreen();
        }            

        public void OnExitButton()
        {
            Application.Quit();
        }

        private void InstallEvents()
        {
            hostManager.FromHostClientExit += OnHostExit;
            clientManager.ClientExit += OnClientExit;
            clientManager.ClientConnected += ShowHostScreen;
        }

        private void HideHostScreen()
        {
            hostManager.HideScreen();
            this.ShowScreen();
        }

        private void OnHostExit()
        {
            if (NetworkManager.Singleton.IsHost)
                hostManager.EndHosting();
            else
                clientManager.DisconnectFromHost();

            hostManager.HideScreen();
            this.ShowScreen();
        }

        private void OnClientExit()
        {
            clientManager.HideScreen();
            this.ShowScreen();
        }

        private void ShowScreen()
        {
            lobbyCanvas.gameObject.SetActive(true);
        }

        private void HideScreen()
        {
            lobbyCanvas.gameObject.SetActive(false);
        }

        private void ShowHostScreen()
        {
            hostManager.ShowScreen();
            clientManager.HideScreen();
        }
    }
}