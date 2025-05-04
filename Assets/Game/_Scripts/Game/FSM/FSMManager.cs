using System;
using System.Collections;
using System.Collections.Generic;
using RTS.Assets.Game._Scripts.Game.FSM.States;
using UnityEngine;

namespace RTS.Assets.Game._Scripts.Game.FSM
{
    public class FSMManager : MonoBehaviour
    {
        private Dictionary<Type, State> _states;
        private State _currentState;
        private bool _isTranslate;

        public bool IsTranslate { get { return _isTranslate; } }

        public void Awake()
        {
            _states = new Dictionary<Type, State>()
            {
                [typeof(BeginState)] = new BeginState(),
                [typeof(GameplayState)] = new GameplayState(),
                [typeof(EndState)] = new EndState()
            };
        }

        public IEnumerator SetState(Type type)
        {
            _isTranslate = true;
            yield return _currentState?.Exit();
            _currentState = _states[type];
            yield return _currentState.Enter();
            _isTranslate = false;
        }

        public Type GetCurrentState()
        {
            return _currentState.GetType();
        }
    }
}
