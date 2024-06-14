using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance;

    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator FadeScreenOut()
    {
        yield return canvasGroup.DOFade(1f, fadeDuration).WaitForCompletion();
    }

    public IEnumerator FadeScreenIn()
    {
        yield return canvasGroup.DOFade(0f, fadeDuration).WaitForCompletion();
    }
}
