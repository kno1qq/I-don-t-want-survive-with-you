using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Womenright : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public GameObject UIPanel;
    public GameObject TalkUI;


    void Start()
    {

        button3.onClick.AddListener(OnButton3Click);
    }

    void Update()
    {

        bool button1Interactable = button1.interactable;
        bool button2Interactable = button2.interactable;


        if (!button1Interactable && !button2Interactable)
        {
            button3.interactable = true;
        }
        else
        {
            button3.interactable = false;
        }
    }

    void OnButton3Click()
    {

        UIPanel.SetActive(false);
        GameManager.instance.openDialog(5);
        GameManager.instance.Button3Clicked("Womenright");
    }
}
