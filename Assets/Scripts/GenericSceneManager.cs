using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GenericSceneManager
{
    private static System.Action _action;
    private static AsyncOperation _operation;
    private static Scene _currentScene;

    public static float? progress { get { return _operation.progress; } }
    public static bool activateScene { set { _operation.allowSceneActivation = value; } }

    public static int currentSceneIndex { get { return _currentScene.buildIndex; } }
    public static string currentSceneName { get { return _currentScene.name; } }


    public static void LoadScene(string sceneName, bool isAdditve = false, System.Action onComplete = null)
    {
        if(string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Null string passed in LoadScene");
            return;
        }

        Debug.Log("Loading scene with name: " + sceneName);
        _action = onComplete;

        SceneManager.LoadSceneAsync(sceneName, isAdditve ? LoadSceneMode.Additive : LoadSceneMode.Single).completed += OnComplete;
    }
    public static void LoadScene(int index, bool isAdditve = false, System.Action onComplete = null)
    {
        if(index > SceneManager.sceneCount || index < 0)
        {
            Debug.LogError("Scene index out of range");
            return;
        }

        Debug.Log("Loading scene with id: " + index);
        _action = onComplete;

        SceneManager.LoadSceneAsync(index, isAdditve ? LoadSceneMode.Additive : LoadSceneMode.Single).completed += OnComplete;

    }

    private static void OnComplete(AsyncOperation async)
    {
        _operation = async;
        _operation.allowSceneActivation = false;
        _currentScene = SceneManager.GetActiveScene();
        _action?.Invoke();
    }
}
