using UnityEngine;

namespace RTS
{
    public class MenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject LobbyUI;

        public void Run()
        {
            LobbyUI.SetActive(true);
        }
    }
}
