using System.Collections;
using RTS.Assets.Game._Scripts.EntryPoints;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RTS.Core.Game
{
    public class GameStart
    {
        public static GameStart Instance;
        private Coroutines _coroutines;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            Application.targetFrameRate = 0;
            Instance = new GameStart();
            Instance.RunGame();
        }

        public static GameStart GetInstance()
        {
            return Instance;
        }

        private GameStart()
        {
            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);
        }

        private void RunGame()
        {
            var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.BOOT)
            {
                _coroutines.StartCoroutine(BootSceneToMenu());
            }
            if (sceneName == Scenes.MENU)
            {
                _coroutines.StartCoroutine(BootSceneToMenu());
            }
        }

        public void ChangeScene(string name)
        {
            if (name == Scenes.DEFAULT_MAP)
            {
                _coroutines.StartCoroutine(MenuSceneToDefaultMap());
            }
        }

        private IEnumerator BootSceneToMenu()
        {
            Debug.Log("3");
            yield return new WaitForEndOfFrame();
            yield return SceneManager.LoadSceneAsync(Scenes.BOOT);
            yield return new WaitForEndOfFrame();
            yield return SceneManager.LoadSceneAsync(Scenes.MENU);
            var sceneEntryPoint = Object.FindFirstObjectByType<MenuEntryPoint>();
            sceneEntryPoint.Run();
        }

        private IEnumerator MenuSceneToDefaultMap()
        {
            yield return new WaitForEndOfFrame();
            //NetworkManager.Singleton.SceneManager.SetClientSynchronizationMode(LoadSceneMode.Single); // <-------- Prevents initial issue
        }
    }
}
