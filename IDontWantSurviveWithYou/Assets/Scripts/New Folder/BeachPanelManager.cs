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
            StartCoroutine(GameManager.instance.showTip("使用鍵盤WASD移動"));
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
            GameManager.instance.openScreenPanel("第一幕:劫後餘生");
            GameManager.instance.GameState = 5;
        }
        if (GameManager.instance.GameState == 5 && _screenPanel.isClose)
        {
            SceneManager.LoadScene("ForestScene");
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
}
