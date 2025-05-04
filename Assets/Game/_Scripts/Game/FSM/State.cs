using System.Collections;

namespace RTS.Assets.Game._Scripts.Game.FSM
{
    public abstract class State
    {
        public abstract IEnumerator Enter();
        public abstract IEnumerator Exit();
    }
}
