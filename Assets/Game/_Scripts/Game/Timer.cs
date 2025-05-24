using System.Text;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace RTS.Core.Game
{
    public class Timer : ITimer
    {
        public async UniTask PauseCountdown()
        {
            await UniTask.Yield();

        }

        public async UniTask SetCountdown(float time, TMP_Text text)
        {
            await RunCountdown(time, text);
        }

        private async UniTask RunCountdown(float time, TMP_Text text)
        {
            StringBuilder sb = new();
            float timeElapsed = 0f;
            
            while (timeElapsed < time)
            {
                timeElapsed += Time.deltaTime;

                sb.Clear();
                text.text = sb.Append($"Time: {Mathf.CeilToInt(time - timeElapsed)}").ToString();
                await UniTask.Yield();
            }
            //sb.Clear();
            //text.text = sb.Append($"Time: {Mathf.CeilToInt(time - timeElapsed)}").ToString();
        }
    }
}
