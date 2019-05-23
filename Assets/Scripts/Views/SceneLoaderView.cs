using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

namespace SceneLoader
{
    public class SceneLoaderView : MonoBehaviour
    {
        [SerializeField] private GameObject m_Container;
        [SerializeField] private TextMeshProUGUI m_LoadingText;
        [SerializeField] private TextMeshProUGUI m_Progress;
        [SerializeField] private Slider m_Slider;

        private bool _isEnabled = false;
        public new bool enabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;

                if (!value)
                    StopAllCoroutines();

                m_Container.SetActive(value);
            }
        }

        private float _loadingTextDuration;
        private bool _sortAccordingToWeight;


        public void Configure(float loadingTextDuration, bool sortAccordingToWeight)
        {
            _loadingTextDuration = loadingTextDuration;
            _sortAccordingToWeight = sortAccordingToWeight;
        }

        public void SetProgress(float progress)
        {
            m_Progress.text = progress + "%";
            m_Slider.value = progress;
        }

        public void ShowLoadingText(List<LoadingText> loadingTexts)
        {
            if (_sortAccordingToWeight)
                loadingTexts = loadingTexts.OrderByDescending(x => x.weight).ToList();

            StartCoroutine(LoadingTextAnimation(loadingTexts));
        }

        private IEnumerator LoadingTextAnimation(List<LoadingText> loadingTexts)
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                foreach(LoadingText loadingText in loadingTexts)
                {
                    m_LoadingText.text = loadingText.text;
                    yield return new WaitForSeconds(_loadingTextDuration);
                }
            }
        }

        private void SetLoadingText(string text)
        {
            m_LoadingText.text = text;
        }
    }
}
