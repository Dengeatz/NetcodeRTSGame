using RTS.Assets.Game._Scripts.Game;
using RTS.Assets.Game._Scripts.Game.FSM;
using RTS.Assets.Game._Scripts.Game.FSM.States;
using UnityEngine;

namespace RTS.Assets.Game._Scripts.EntryPoints
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private FSMManager _fsmManager;
        
        public void Awake()
        {
            ServiceLocator.Register<FSMManager>(_fsmManager);
        }
    }
}
