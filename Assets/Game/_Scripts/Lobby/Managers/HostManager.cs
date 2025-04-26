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

        private Dictionary<ulong, LobbyPlayer> playerList = new Dictionary<ulong, LobbyPlayer>();

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

            foreach (var player in playerList.Values)
            {
                Destroy(player.gameObject);
            }
            playerList.Clear();
        }

        private void OnConnectionEvent(NetworkManager manager, ConnectionEventData data)
        {
            if (data.EventType == ConnectionEvent.ClientConnected && manager.IsHost)
                ClientConnected(data.ClientId);
            
            if ((data.EventType == ConnectionEvent.ClientConnected || data.EventType == ConnectionEvent.PeerConnected) && !manager.IsHost)
                ClientConnectedForClient();

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
            playerList.Add(obj, GameObject.Instantiate(clientPrefab, clientListTransform).GetComponent<LobbyPlayer>());
            var gObj = playerList[obj];
            gObj.PlayerName.text = obj.ToString();
        }

        private void ClientDisconnected(ulong obj)
        {
            var gObj = playerList[obj];
            playerList.Remove(obj);
            Destroy(gObj.gameObject);
        }


        private void ClientConnectedForClient()
        {
            foreach(var client in NetworkManager.Singleton.ConnectedClientsIds)
            {
                if (playerList.ContainsKey(client)) continue;

                playerList.Add(client, GameObject.Instantiate(clientPrefab, clientListTransform).GetComponent<LobbyPlayer>());
                var gObj = playerList[client];
                gObj.PlayerName.text = client.ToString();
            }
        }

        private void ClientDisconnectedForClient()
        {
            foreach (var player in playerList.Values)
            {
                Destroy(player.gameObject);
            }
            playerList.Clear();
            NetworkManager.Singleton.Shutdown();
            FromHostClientExit?.Invoke();
        }
    }
}
