using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

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


        [Rpc(SendTo.Server)] 
        public void ChangeNameRpc()
        {
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
