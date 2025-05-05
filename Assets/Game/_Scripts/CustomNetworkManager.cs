using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RTS
{
    public class CustomNetworkManager : MonoBehaviour
    {
        [SerializeField] private GameObject networkPrefab;
        private GameObject networkInstance;

        public void Awake()
        {
            if (networkInstance == null && GameObject.FindAnyObjectByType<NetworkManager>() == null)
            {
                networkInstance = GameObject.Instantiate(networkPrefab);
                DontDestroyOnLoad(networkInstance);
            }
        }
    }
}
