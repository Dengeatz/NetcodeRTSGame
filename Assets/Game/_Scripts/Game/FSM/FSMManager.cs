using System;
using System.Collections.Generic;
using RTS.Assets.Game._Scripts.Game.FSM.States;
using RTS.Assets.Game._Scripts.Game.FSM.States.Enums;
using Unity.Netcode;
using UnityEngine;

namespace RTS.Assets.Game._Scripts.Game.FSM
{
    public class FSMManager : NetworkBehaviour
    {
        private Dictionary<StatesEnum, State> _states;
        private State _currentState;
        private NetworkVariable<StatesEnum> _toSet = new NetworkVariable<StatesEnum>();

        public void Awake()
        {
            _states = new Dictionary<StatesEnum, State>()
            {
                [StatesEnum.Begin] = new BeginState(),
                [StatesEnum.Gameplay] = new GameplayState(),
                [StatesEnum.End] = new EndState()
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
            _currentState?.Exit();
            _currentState = _states[_toSet.Value];
            _currentState.Enter();
        }

        public Type GetCurrentState()
        {
            return _currentState.GetType();
        }
    }
}
