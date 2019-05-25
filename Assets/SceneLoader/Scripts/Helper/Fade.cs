using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private Image m_FadeImage;
    private float _fadeDuration = 1.5f;

    public System.Action OnComplete;

    public void SetFadeDuration(float duration)
    {
        _fadeDuration = duration;
    }

    [ContextMenu("FadeIn")]
    public void FadeIn()
    {
        StartCoroutine(FadeTo(1.0f, 1.0f));
    }

    [ContextMenu("FadeOut")]
    public void FadeOut()
    {
        StartCoroutine(FadeTo(0.0f, 1.0f));
    }

    IEnumerator FadeTo(float alphaValue, float time)
    {
        float alpha = m_FadeImage.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            Color newColor = new Color(m_FadeImage.color.r,
                                        m_FadeImage.color.g,
                                        m_FadeImage.color.b,
                                        Mathf.Lerp(alpha, alphaValue, t));

            m_FadeImage.color = newColor;
            yield return null;
        }

        OnComplete?.Invoke();
    }
}
