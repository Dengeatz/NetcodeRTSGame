using RTS.Assets.Game._Scripts.Game.Enums;
using RTS.Assets.Game._Scripts.Game.PlayerScripts;
using RTS.Assets.Game._Scripts.Game.PlayerScripts.Interfaces;
using Unity.Netcode;
using UnityEngine;

namespace RTS.Assets.Game._Scripts.Game
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private Camera _camera;

        public Team PlayerTeam;
        private PlayerCamera _cameraHandler;
        private IInput _inputHandler;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            _inputHandler = new PlayerInput();
            _cameraHandler = new PlayerCamera(_inputHandler, this.transform.gameObject, _camera, 100f, 100f);
        }

        private void Update()
        {
            _cameraHandler.Update();
        }
    }
}
