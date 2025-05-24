using Cysharp.Threading.Tasks;

namespace RTS.Assets.Game._Scripts.Game.FSM.States
{
    public class GameplayState : State
    {
        private FSMManager _parent;

        public GameplayState(FSMManager manager) : base(manager)
        {
            _parent = manager;
        }

        public override async UniTask Enter()
        {
            await UniTask.WaitForSeconds(5f);

            UnityEngine.Debug.Log("Game started");
        }

        public override async UniTask Exit()
        {
            await UniTask.Yield();
        }
    }
}
