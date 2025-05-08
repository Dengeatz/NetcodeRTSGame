using RTS.Assets.Game._Scripts.Game.PlayerScripts.Interfaces;
using UnityEngine;

namespace RTS.Assets.Game._Scripts.Game.PlayerScripts
{
    public class PlayerCamera
    {
        private GameObject _parent;
        private Camera _mainCamera;
        private IInput _input;
        private float _freeAreaWidth = 100f;
        private float _freeAreaHeight = 100f;
        private Vector2 _currentRotationAxis = Vector2.zero;

        public PlayerCamera(IInput input, GameObject parent,Camera camera, float freeAreaWidth, float freeAreaHeight)
        {
            _mainCamera = camera;
            _freeAreaWidth = freeAreaWidth;
            _freeAreaHeight = freeAreaHeight;
            _input = input;
            _parent = parent;
        }

        public void Update()
        {
            CameraMove(CalculatePosition(_input.GetCameraPositionTransformation()));
            CameraRotate(CalculateRotation(_input.GetCameraRotationTransformation()));
        }

        private Vector3 CalculatePosition(Vector2 inputAxis)
        {
            return _parent.transform.position + (_parent.transform.forward * inputAxis.x) + (_parent.transform.right * inputAxis.y);
        }

        private Vector2 CalculateRotation(Vector2 inputAxis)
        {
            return inputAxis;
        }

        private void CameraMove(Vector3 newPosition)
        {
            _parent.transform.position = newPosition;
        }

        private void CameraRotate(Vector2 rotation)
        {
            _mainCamera.transform.Rotate(Vector3.up, -rotation.x, Space.World);
            _mainCamera.transform.Rotate(Vector3.right, rotation.y, Space.World);
        }
    }
}
