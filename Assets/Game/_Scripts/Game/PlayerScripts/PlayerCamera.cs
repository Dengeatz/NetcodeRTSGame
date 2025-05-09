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
            return _parent.transform.position + ((_mainCamera.transform.forward - new Vector3(0, _mainCamera.transform.forward.y - _parent.transform.forward.y, 0)) * inputAxis.y) + ((_mainCamera.transform.right - new Vector3(0, _mainCamera.transform.right.y - _parent.transform.right.y, 0)) * inputAxis.x);
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
            _currentRotationAxis.x += rotation.x;
            _currentRotationAxis.y += rotation.y;
            _mainCamera.transform.rotation = Quaternion.Euler(_currentRotationAxis.y, -_currentRotationAxis.x, 0);
        }
    }
}
