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
}