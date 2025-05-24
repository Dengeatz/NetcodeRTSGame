using System.Data;
using Cysharp.Threading.Tasks;
using TMPro;

namespace RTS.Core.Game
{
    public interface ITimer
    {
        UniTask SetCountdown(float time, TMP_Text text);
        UniTask PauseCountdown();

    }
}
