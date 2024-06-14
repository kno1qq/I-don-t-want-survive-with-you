using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForestPanelManager5 : MonoBehaviour
{
    public GameObject TimerPanel;
    public string sceneToLoad;
    private void Start()
    {
        GameManager.instance.openDialog(0);
        GameManager.instance.GameState = 1;
    }
    private void Update()
    {
        if (GameManager.instance.GameState == 1 && !GameManager.instance.isTalk)
        {
            TimerPanel.SetActive(true);
            GameManager.instance.GameState = 2;
        }
        if (GameManager.instance.GameState == 3 && !GameManager.instance.isTalk)
        {
            Debug.Log("GO");
            StartCoroutine(TransitionCoroutine(sceneToLoad));
        }
        Debug.Log(GameManager.instance.GameState);
    }
    public IEnumerator TransitionCoroutine(string newSceneName)
    {
        yield return StartCoroutine(ScreenFader.Instance.FadeScreenOut());

        SceneManager.LoadScene(newSceneName);

        yield return StartCoroutine(ScreenFader.Instance.FadeScreenIn());
    }
}
