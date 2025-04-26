using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RTS
{
    public class GameStart
    {
        private static GameStart _instance;
        private Coroutines _coroutines;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            Application.targetFrameRate = 0;
            _instance = new GameStart();
            _instance.RunGame();
        }

        public static GameStart GetInstance()
        {
            return _instance;
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

        //public void ChangeScene(string name)
        //{
        //    if (name == Scenes.MENU)
        //    {
        //        _coroutines.StartCoroutine(BootSceneToMenu());
        //    }
        //}

        private IEnumerator BootSceneToMenu()
        {
            yield return new WaitForEndOfFrame();
            yield return SceneManager.LoadSceneAsync(Scenes.BOOT);
            yield return new WaitForEndOfFrame();
            yield return SceneManager.LoadSceneAsync(Scenes.MENU);
            var sceneEntryPoint = Object.FindFirstObjectByType<MenuEntryPoint>();
            sceneEntryPoint.Run();
        }
    }
}
