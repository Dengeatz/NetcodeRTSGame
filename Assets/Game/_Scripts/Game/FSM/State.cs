using System.Collections;
using Cysharp.Threading.Tasks;

namespace RTS.Assets.Game._Scripts.Game.FSM
{
    public abstract class State
    {
        public State(FSMManager fsmManager) { }

        public abstract UniTask Enter();
        public abstract UniTask Exit();
    }
}
