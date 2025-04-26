
using UnityEngine;
using Zenject;

namespace RTS
{
    public class CustomNetworkManager : MonoBehaviour
    {
        public void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
