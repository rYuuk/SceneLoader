﻿using System.Collections;
using UnityEngine;

namespace SceneLoader
{
    public class SceneLoaderController : MonoBehaviour
    {
        [Tooltip("Loading text duration on screen.")]
        [SerializeField] private float m_LoadingTextDuration = 5f;
        [Tooltip("Should loading text be sorted on weight basis.")]
        [SerializeField] private bool m_SortLoadingText = true;
        [SerializeField] private bool m_ShowFade = true;
        [SerializeField] private float m_FadeDuration = 1.5f;

        private SceneLoaderView _view;
        private SceneLoaderModel _model;
        private Fade _fade;

        private Coroutine _loadingProgressCoroutine;
        private AsyncOperation _sceneLoadingAsyncOperation;

        /// <summary>
        /// Callback for scene load completion.
        /// If 'allowSceneActivation' is false, will be called at 90%(scene loaded) because,
        /// it goes to 1 only after 'allowSceneActivation' for scene's async operation is set to true.
        /// Else callback will be called when scene load is complete.
        /// </summary>
        public System.Action onSceneLoadComplete;

        private void Awake()
        {
            //Had to persist in every scene.
            DontDestroyOnLoad(this);
            _model = new SceneLoaderModel();
            _view = FindObjectOfType<SceneLoaderView>();
            _view.Configure(m_LoadingTextDuration, m_SortLoadingText);

            _fade = FindObjectOfType<Fade>();
            _fade.SetFadeDuration(m_FadeDuration);
        }

        /// <summary>
        /// Sets loading view progress.
        /// </summary>
        private IEnumerator SetLoadingProgress()
        {
            while (true)
            {
                if (_sceneLoadingAsyncOperation == null)
                {
                    yield return null;
                    continue;
                }

                _view.SetProgress((_sceneLoadingAsyncOperation.progress));

                if (_sceneLoadingAsyncOperation.isDone)
                {
                    OnSceneLoadCompleted(null);
                    onSceneLoadComplete?.Invoke();
                    yield break;
                }

                if (!_sceneLoadingAsyncOperation.allowSceneActivation && Equals(_sceneLoadingAsyncOperation.progress, 0.9f))
                {
                    onSceneLoadComplete?.Invoke();
                    yield break;
                }
                yield return null;
            }
        }

        /// <summary>
        /// Loads scene with given name.
        /// </summary>
        /// <param name="sceneName">Name of scene to load.</param>
        /// <param name="showLoadingView">Show loading screen while loading.</param>
        /// <param name="allowSceneActivation">Allow scene to activate immediately after loading is complete.</param>
        /// <param name="isAdditve">Is scene have to be loaded additively.</param>
        /// <returns></returns>
        public void Load(string sceneName, bool showLoadingView = true, bool allowSceneActivation = true, bool isAdditve = false)
        {
            if (_loadingProgressCoroutine != null)
                StopCoroutine(_loadingProgressCoroutine);

            if (m_ShowFade)
            {
                _fade.OnComplete = () =>
                {
                    _fade.OnComplete = null;
                    LoadScene(sceneName, showLoadingView, allowSceneActivation, isAdditve);
                };
                _fade.FadeIn();
            }
            else
                LoadScene(sceneName, showLoadingView, allowSceneActivation, isAdditve);
        }

        private void LoadScene(string sceneName, bool showLoadingView, bool allowSceneActivation, bool isAdditve)
        {
            if (showLoadingView)
            {
                _view.enabled = true;
                _view.ShowLoadingText(_model.loadingTexts);
                _view.SetProgress(0);
                _loadingProgressCoroutine = StartCoroutine(SetLoadingProgress());
            }

            _sceneLoadingAsyncOperation = SceneLoader.LoadScene(sceneName, isAdditve);
            _sceneLoadingAsyncOperation.allowSceneActivation = allowSceneActivation;
            if (allowSceneActivation)
                _sceneLoadingAsyncOperation.completed += OnSceneLoadCompleted;
        }


        /// <summary>
        /// Call this if 'allowSceneActivation' is true in Load()'s parameters.
        /// Activates scene, if scene is already loaded, else sets scene for 
        /// immediate activation after loading is complete.
        /// </summary>
        public void ActivateScene()
        {
            _sceneLoadingAsyncOperation.completed += OnSceneLoadCompleted;
            _sceneLoadingAsyncOperation.allowSceneActivation = true;
        }

        private void OnSceneLoadCompleted(AsyncOperation obj)
        {
            _view.enabled = false;
            _sceneLoadingAsyncOperation.completed -= OnSceneLoadCompleted;
            if (m_ShowFade)
                _fade.FadeOut();
        }
    }
}