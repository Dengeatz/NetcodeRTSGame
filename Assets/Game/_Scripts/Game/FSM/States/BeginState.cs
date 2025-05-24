using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace RTS.Assets.Game._Scripts.Game.FSM.States
{
    public class BeginState : State
    {
        private FSMManager _parent;

        public BeginState(FSMManager manager) : base(manager)
        {
            _parent = manager;
        }

        public override async UniTask Enter()
        {
            await _parent.Timer.SetCountdown(5f, _parent.TimerText);
            
            await _parent.NotifyGUI.SetNotify("BeginState Ended!");
            
            Debug.Log("Game started");
        }

        public override async UniTask Exit()
        {
            await UniTask.Yield();
        }
    }
}
