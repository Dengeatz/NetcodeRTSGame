using RTS.Assets.Game._Scripts.Game.Enums;
using RTS.Assets.Game._Scripts.Game.PlayerScripts;
using RTS.Assets.Game._Scripts.Game.PlayerScripts.Interfaces;
using Unity.Netcode;
using UnityEngine;

namespace RTS.Assets.Game._Scripts.Game
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] private GameObject _cameraObject;

        public Team PlayerTeam;
        private PlayerCamera _cameraHandler;
        private IInput _inputHandler;
        private Camera _camera;

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                _camera = _cameraObject.AddComponent<Camera>();
                base.OnNetworkSpawn();
                _inputHandler = new PlayerInput();
                _cameraHandler = new PlayerCamera(_inputHandler, this.transform.gameObject, _camera, 100f, 100f);
            }
        }

        private void Update()
        {
            if(IsOwner)
                _cameraHandler.Update();
        }
    }
}
