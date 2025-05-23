using System.ComponentModel;
using RTS.Core.Game;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RTS
{
    public class LobbyPlayer : NetworkBehaviour
    {
        private NetworkVariable<FixedString64Bytes> _name = new NetworkVariable<FixedString64Bytes>();
        [SerializeField] public TMP_Text PlayerName;

        public override void OnNetworkSpawn()
        {
            if(IsOwner)
                ChangeName();
        }

        private void ChangeName()
        {
            ChangeNameRpc();
        }

        public void GameStarted()
        {
            if(IsServer)
                GameStartedRpc();
        }

        [Rpc(SendTo.Server)]
        private void GameStartedRpc()
        {
            //foreach(var uid in NetworkManager.Singleton.ConnectedClientsIds)
            //{
            //    NetworkManager.Singleton.ConnectedClients[uid].PlayerObject.Despawn(true);
            //}
            NetworkManager.Singleton.SceneManager.LoadScene(Scenes.DEFAULT_MAP, LoadSceneMode.Single);
        }

        [Rpc(SendTo.Server)] 
        public void ChangeNameRpc()
        {
            this.transform.localScale = Vector3.one;
            _name.Value = PlayerPrefs.GetString("Name");
        }

        private void Update()
        {
            PlayerName.text = _name.Value.ToString();
        }

        //public static LobbyPlayer CreatePlayer(GameObject clientPrefab, Transform clientListTransform)
        //{
        //    return GameObject.Instantiate(clientPrefab, clientListTransform).GetComponent<LobbyPlayer>();
        //}
    }
}
