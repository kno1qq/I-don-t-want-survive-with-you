using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeachPanelManager : MonoBehaviour
{
    public bool isShowedTip = false;
    public bool isShowedDialog = false;

    public GameObject dialogPanel;
    public ScreenPanel _screenPanel;

    public int state = 0;
    public string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.openDialog(0);
        dialogPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GameState == 0 && !GameManager.instance.isTalk)
        {
            StartCoroutine(GameManager.instance.showTip("�ϥ���LWASD����"));
            isShowedTip = true;
            GameManager.instance.GameState += 1;
        }
        if (GameManager.instance.GameState == 3 && !GameManager.instance.isTalk)
        {
            GameManager.instance.GameState = 4;
            GameManager.instance.openDialog(3);
        }
        if (GameManager.instance.GameState == 4 && !GameManager.instance.isTalk)
        {
            GameManager.instance.openScreenPanel("�Ĥ@��:�T��l��");
            StartCoroutine(TransitionCoroutine(sceneToLoad));
            GameManager.instance.GameState = 5;
        }
        if (GameManager.instance.GameState == 5 && _screenPanel.isClose)
        {
            
        }
    }
    /*
    IEnumerator State(string tipText)
    {
        GameManager.instance.OpenTipPanel(tipText);
        yield return new WaitForSeconds(3);
        GameManager.instance.CloseTipPanel();
    }
    */
    public IEnumerator TransitionCoroutine(string newSceneName)
    {
        yield return StartCoroutine(ScreenFader.Instance.FadeScreenOut());

        SceneManager.LoadScene(newSceneName);

        yield return StartCoroutine(ScreenFader.Instance.FadeScreenIn());
    }
}
