using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    public class HostManager : MonoBehaviour
    {
        [SerializeField] private Canvas hostCanvas;
        [SerializeField] private Transform clientListTransform;
        [SerializeField] private GameObject clientPrefab;

        private List<ulong> playerList = new List<ulong>();

        public Action FromHostClientExit;

        private void OnEnable()
        {
            NetworkManager.Singleton.OnConnectionEvent += OnConnectionEvent;
        }


        private void OnDisable()
        {
            NetworkManager.Singleton.OnConnectionEvent -= OnConnectionEvent;
        }

        public void ShowScreen()
        {
            hostCanvas.gameObject.SetActive(true);
        }

        public void HideScreen()
        {
            hostCanvas.gameObject.SetActive(false);
        }

        public void OnHideButton()
        {
            FromHostClientExit?.Invoke();
        }

        public void StartHosting()
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
                "127.0.0.1",
                (ushort)7777,
                "0.0.0.0"
            );

            NetworkManager.Singleton.StartHost();
        }

        public void EndHosting()
        {
            NetworkManager.Singleton.Shutdown();

            playerList.Clear();
        }

        private void OnConnectionEvent(NetworkManager manager, ConnectionEventData data)
        {
            if (data.EventType == ConnectionEvent.ClientConnected && manager.IsHost)
                ClientConnected(data.ClientId);
            
            if ((data.EventType == ConnectionEvent.ClientConnected || data.EventType == ConnectionEvent.PeerConnected) && !manager.IsHost)
                ClientConnectedForClient(data.ClientId);

            if (data.EventType == ConnectionEvent.ClientDisconnected && manager.IsHost)
                if (NetworkManager.Singleton.CurrentSessionOwner == manager.LocalClientId)
                    return;
                else
                    ClientDisconnected(data.ClientId);

            if (data.EventType == ConnectionEvent.ClientDisconnected && !manager.IsHost)
                ClientDisconnectedForClient();

            if (data.EventType == ConnectionEvent.PeerDisconnected && !manager.IsHost)
                ClientDisconnected(data.ClientId);

            //Debug.Log($"{manager.IsHost}, {data.EventType}");
        }

        private void ClientConnected(ulong obj)
        {
            NetworkManager.Singleton.ConnectedClients[obj].PlayerObject.transform.parent = clientListTransform;
            playerList.Add(obj);
        }

        private void ClientDisconnected(ulong obj)
        {
            playerList.Remove(obj);
        }


        private void ClientConnectedForClient(ulong id)
        {
            foreach(var client in NetworkManager.Singleton.ConnectedClientsIds)
            {
                if (playerList.Contains(client)) continue;

                playerList.Add(client);
            }
        }

        private void ClientDisconnectedForClient()
        {
            playerList.Clear();
            NetworkManager.Singleton.Shutdown();
            NetworkManager.Singleton.ConnectedClients[NetworkManager.Singleton.LocalClientId].PlayerObject.Despawn();
            FromHostClientExit?.Invoke();
        }
    }
}
