using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager instance;

    public bool isShowedTip = false;
    public bool isShowedDialog = false;
    
    public GameObject dialogPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.dialogPart == 1 && !isShowedTip)
        {
            StartCoroutine(showTip("�ϥ���LWASD����"));
            isShowedTip = true;
        }

        if (GameManager.instance.dialogPart == 3)
        {
            //vCam.m_Lens.OrthographicSize = 3;
            dialogPanel.SetActive(true);
        }
        if (GameManager.instance.dialogPart == 4)
        {
            GameManager.instance.dialogPart = 5;
            GameManager.instance.openScreenPanel("�Ĥ@��:�T��l��");
            StartCoroutine(showTip("�j�M���y�W������ôM��i�H���窺�a��"));
            GameManager.instance.openGrid();
        }
    }
    IEnumerator showTip(string tipText)
    {
        GameManager.instance.OpenTipPanel(tipText);
        yield return new WaitForSeconds(3);
        GameManager.instance.CloseTipPanel();
    }
}
