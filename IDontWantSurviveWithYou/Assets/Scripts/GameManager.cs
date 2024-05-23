using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }
    void Update()
    {
        
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
    public void closePanel()
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
}