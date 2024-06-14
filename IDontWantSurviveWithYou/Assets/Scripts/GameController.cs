using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameObject Button;
    public string sceneToLoad;
    public void RepalyGame()
    {
        StartCoroutine(TransitionCoroutine(sceneToLoad));
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("LOBBY");
    }

    public IEnumerator TransitionCoroutine(string newSceneName)
    {
        yield return StartCoroutine(ScreenFader.Instance.FadeScreenOut());

        SceneManager.LoadScene(newSceneName);

        yield return StartCoroutine(ScreenFader.Instance.FadeScreenIn());
    }
}
