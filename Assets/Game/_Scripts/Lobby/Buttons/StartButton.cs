using System;
using UnityEngine;
using UnityEngine.UI;

namespace RTS
{
    public class StartButton : MonoBehaviour
    {
        private Button button;
        public Action StartButtonPressed;

        void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(Pressed);
        }

        void Pressed()
        {
            StartButtonPressed?.Invoke();
        }
    }
}
