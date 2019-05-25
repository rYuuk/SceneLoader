using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneLoader
{
    public class SceneLoaderModel
    {
        private LoadingTextManager _loadingTextManager;

        public List<LoadingText> loadingTexts { get { return _loadingTextManager.loadingTexts; } }

        public SceneLoaderModel()
        {
            _loadingTextManager = new LoadingTextManager();
        }
    }
}
