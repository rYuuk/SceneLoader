using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoader
{
    public class SceneLoader
    {
        public static AsyncOperation LoadScene(string sceneName, bool isAdditve = false)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogWarning("#SceneLoader, Null string passed in LoadScene");
                return null;
            }

            Debug.Log("#SceneLoader, Loading scene with name: " + sceneName);

            if (Application.CanStreamedLevelBeLoaded(sceneName))
                return SceneManager.LoadSceneAsync(sceneName, isAdditve ? LoadSceneMode.Additive : LoadSceneMode.Single);
            else
            {
                Debug.LogWarning("#SceneLoader, No such scene");
                return null;
            }
        }

        public static AsyncOperation UnloadScene(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogWarning("#SceneLoader, Null string passed in LoadScene");
                return null;
            }

            Debug.Log("#SceneLoader, Loading scene with name: " + sceneName);
            if (Application.CanStreamedLevelBeLoaded(sceneName))
                return SceneManager.UnloadSceneAsync(sceneName);
            else
            {
                Debug.LogWarning("#SceneLoader, No such scene");
                return null;
            }
        }
    }
}
