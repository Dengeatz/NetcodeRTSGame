using System;
using System.Collections;

namespace RTS.Assets.Game._Scripts.Game.FSM.States
{
    public class BeginState : State
    {
        public override IEnumerator Enter()
        {
            UnityEngine.Debug.Log("Game started!");
            yield return null;
        }

        public override IEnumerator Exit()
        {
            yield return null;
        }
    }
}
