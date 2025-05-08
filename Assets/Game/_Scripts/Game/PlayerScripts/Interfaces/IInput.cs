
using UnityEngine;

namespace RTS.Assets.Game._Scripts.Game.PlayerScripts.Interfaces
{
    public interface IInput
    {
        Vector2 GetCameraPositionTransformation();
        Vector2 GetCameraRotationTransformation();
    }
}
