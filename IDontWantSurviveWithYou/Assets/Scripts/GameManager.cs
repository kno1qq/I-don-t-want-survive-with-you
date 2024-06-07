using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("UI")]
    public int dialogPart = 0;
    public GameObject tipPanel;
    public Text tipText;
    public GameObject screenPanel;
    public Text screenPanelText;
    public GameObject grid;
    public GameObject dialogPanel;

    public GameObject player;

    public bool isTalk = false;
    public int GameState;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }
    public void OpenTipPanel(string text)
    {
        tipText.text = text;
        tipPanel.SetActive(true);
    }
    public void CloseTipPanel()
    {
        tipPanel.SetActive(false);
    }
    //第一幕第二幕第三幕
    public void openScreenPanel(string text)
    {
        screenPanelText.text = text;
        screenPanel.SetActive(true);
    }
    public void closeScreenPanel()
    {
        screenPanel.SetActive(false);
    }
    public void openGrid()
    {
        grid.SetActive(true);
    }
    public void closeGrid()
    {
        grid.SetActive(false);
    }
    public void openDialog(int part)
    {
        isTalk = true;
        dialogPart = part;
        dialogPanel.SetActive(true);
    }
    public IEnumerator showTip(string tipText)
    {
        instance.OpenTipPanel(tipText);
        yield return new WaitForSeconds(3);
        instance.CloseTipPanel();
    }
}