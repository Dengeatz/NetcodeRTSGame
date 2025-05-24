using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace RTS
{
    public class NotifyGUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_notifyText;

        public async UniTask SetNotify(string notifyText)
        {
            m_notifyText.text = notifyText;

            for(float i = 0; i < 1.0f; i += Time.deltaTime)
            {
                m_notifyText.alpha = i;
                await UniTask.Yield();
            }
            
            m_notifyText.alpha = 1.0f;

            await UniTask.WaitForSeconds(2f);

            for (float i = 1.0f; i > 0f; i -= Time.deltaTime)
            {
                m_notifyText.alpha = i;
                await UniTask.Yield();
            }

            m_notifyText.alpha = 0.0f;
        }
    }
}
