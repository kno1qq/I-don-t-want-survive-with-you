using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject uiPanel;       // 指定的UI面板
    public GameObject promptTextUI;  // 提示文本UI

    private bool isPlayerNearby = false;

    void Start()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false); // 初始化時隱藏UI面板
        }
        if (promptTextUI != null)
        {
            promptTextUI.SetActive(false); // 初始化時隱藏提示文本
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.R))
        {
                      
            if (uiPanel != null)
            {
                uiPanel.SetActive(!uiPanel.activeSelf); // 切換UI面板顯示狀態
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {           
            isPlayerNearby = true;
            if (promptTextUI != null)
            {
                promptTextUI.SetActive(true); // 顯示提示文本
            }
        }

        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {           
            if (promptTextUI != null)
            {
                promptTextUI.SetActive(false); // 隱藏提示文本
            }
        }
    }

    
}
