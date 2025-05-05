using System.Collections;

namespace RTS.Assets.Game._Scripts.Game.FSM
{
    public abstract class State
    {
        public abstract void Enter();
        public abstract void Exit();
    }
}
