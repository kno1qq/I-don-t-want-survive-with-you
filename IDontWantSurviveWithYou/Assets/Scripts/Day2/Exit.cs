using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    bool isdone;
    public string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        isdone = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isdone)
        {
            StartCoroutine(TransitionCoroutine(sceneToLoad));
            isdone = true;
        }
    }

    public IEnumerator TransitionCoroutine(string newSceneName)
    {
        yield return StartCoroutine(ScreenFader.Instance.FadeScreenOut());

        SceneManager.LoadScene(newSceneName);

        yield return StartCoroutine(ScreenFader.Instance.FadeScreenIn());
    }
}
