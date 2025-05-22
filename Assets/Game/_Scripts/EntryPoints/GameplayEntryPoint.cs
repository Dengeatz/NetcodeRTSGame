using RTS.Assets.Game._Scripts.Game;
using RTS.Assets.Game._Scripts.Game.FSM;
using RTS.Assets.Game._Scripts.Game.FSM.States;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;

namespace RTS.Assets.Game._Scripts.EntryPoints
{
    public class GameplayEntryPoint : NetworkBehaviour
    {
        [SerializeField] private FSMManager _fsmManager;
        [SerializeField] private NetworkObject _playerPrefab;

        public void Awake()
        {
            ServiceLocator.Register<FSMManager>(_fsmManager);
            ServiceLocator.Register<TeamManager>(new TeamManager());
        }

        public override void OnNetworkSpawn()
        {
            SpawnPlayersRpc();
        }

        [Rpc(SendTo.Server)]
        public void SpawnPlayersRpc(RpcParams par = default)
        {
            Debug.Log(par.Receive.SenderClientId);
            ulong senderClientId = par.Receive.SenderClientId;
            var playerObject = NetworkManager.Singleton.SpawnManager.InstantiateAndSpawn(_playerPrefab, par.Receive.SenderClientId, false, true);
            playerObject.ChangeOwnership(senderClientId);
            ServiceLocator.GetService<TeamManager>().AddPlayer(_playerPrefab.GetComponent<Player>());
        }
    }
}
