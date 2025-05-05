using System;
using System.Collections;

namespace RTS.Assets.Game._Scripts.Game.FSM.States
{
    public class BeginState : State
    {
        public override void Enter()
        {
            UnityEngine.Debug.Log("Game started!");
        }

        public override void Exit()
        {
        }
    }
}
