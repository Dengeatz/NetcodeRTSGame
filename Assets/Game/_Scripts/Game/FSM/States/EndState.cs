using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RTS.Assets.Game._Scripts.Game.FSM.States
{
    public class EndState : State
    {
        private FSMManager _parent;

        public EndState(FSMManager manager) : base(manager)
        {
            _parent = manager;
        }

        public override async UniTask Enter()
        {
            await UniTask.WaitForSeconds(5f);

            Debug.Log("Game started");
        }

        public override async UniTask Exit()
        {
            await UniTask.Yield();
        }
    }
}
