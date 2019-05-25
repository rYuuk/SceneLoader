using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneLoader;

public class BasicExample : MonoBehaviour
{
    [SerializeField] private string sceneName = "SceneB";
    [SerializeField] private bool allowSceneActivation = true;

    private SceneLoaderController _controller;

    private void Start()
    {
        _controller = GameObject.FindObjectOfType<SceneLoaderController>();
        if(allowSceneActivation)
            _controller.onSceneLoadComplete += _controller.ActivateScene;
    }

    public void SwitchScene()
    {
        _controller.Load(sceneName, true, false);
    }
}
