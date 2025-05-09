using RTS.Assets.Game._Scripts.Game.PlayerScripts.Interfaces;
using UnityEngine;

namespace RTS.Assets.Game._Scripts.Game.PlayerScripts
{
    public class PlayerInput : IInput
    {
        public Vector2 GetCameraPositionTransformation()
        {
            return GetKeyboardAxis();
        }

        public Vector2 GetCameraRotationTransformation()
        {
            if (IsRightMBHolds())
            {
                return GetMouseAxis();
            }
            return Vector2.zero;
        }

        private bool IsMiddleMBHolds()
        {
            return Input.GetKey(KeyCode.Mouse2);
        }

        private bool IsRightMBHolds()
        {
            return Input.GetKey(KeyCode.Mouse1);
        }

        private Vector2 GetMouseAxis()
        {
            return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

        private Vector2 GetKeyboardAxis()
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }
}
