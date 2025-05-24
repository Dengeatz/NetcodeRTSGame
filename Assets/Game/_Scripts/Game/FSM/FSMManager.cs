using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using RTS.Assets.Game._Scripts.Game.FSM.States;
using RTS.Assets.Game._Scripts.Game.FSM.States.Enums;
using RTS.Core.Game;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace RTS.Assets.Game._Scripts.Game.FSM
{
    public class FSMManager : NetworkBehaviour
    {
        [SerializeField] public TMP_Text TimerText;

        [Header("General")]
        [SerializeField] public NotifyGUI NotifyGUI;
        
        [Header("BeginState")]

        private Dictionary<StatesEnum, State> _states;
        private State _currentState;
        private NetworkVariable<StatesEnum> _toSet = new NetworkVariable<StatesEnum>();

        public ITimer Timer;

        public void Awake()
        {
            Timer = new Timer();
            _states = new Dictionary<StatesEnum, State>()
            {
                [StatesEnum.Begin] = new BeginState(this),
                [StatesEnum.Gameplay] = new GameplayState(this),
                [StatesEnum.End] = new EndState(this)
            };
        }

        public override void OnNetworkSpawn()
        {
            if(IsOwner)
                SetState(StatesEnum.Begin);
        }

        public void SetState(StatesEnum type)
        {
            if (NetworkManager.Singleton.IsHost)
            {
                _toSet.Value = type;
                SetStateRpc();
            }
        }

        [Rpc(SendTo.Everyone)]
        public void SetStateRpc()
        {
            UniTask.Void(SetStateAsync);
        }

        public async UniTaskVoid SetStateAsync()
        {
            if(_currentState != null)
                await _currentState.Exit();
            
            _currentState = _states[_toSet.Value];
            await _currentState.Enter();
        }


        public Type GetCurrentState()
        {
            return _currentState.GetType();
        }
    }
}
