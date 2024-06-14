using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForestPanelManager2 : MonoBehaviour
{
    public string sceneToLoad;
    private void Start()
    {
        GameManager.instance.openDialog(0);
        GameManager.instance.GameState = 1;
    }
    private void Update()
    {
        if (GameManager.instance.GameState == 15)
        {
            GameManager.instance.openDialog(2);
            GameManager.instance.GameState = 16;
        }
        if (GameManager.instance.GameState == 16 && !GameManager.instance.isTalk)
        {
            StartCoroutine(TransitionCoroutine(sceneToLoad));
        }
    }
    public IEnumerator TransitionCoroutine(string newSceneName)
    {
        yield return StartCoroutine(ScreenFader.Instance.FadeScreenOut());

        SceneManager.LoadScene(newSceneName);

        yield return StartCoroutine(ScreenFader.Instance.FadeScreenIn());
    }
}
