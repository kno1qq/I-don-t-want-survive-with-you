using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public GameObject uiPanel;       // ���w��UI���O
    public GameObject promptTextUI;  // ���ܤ奻UI

    private bool isPlayerNearby = false;

    void Start()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false); // ��l�Ʈ�����UI���O
        }
        if (promptTextUI != null)
        {
            promptTextUI.SetActive(false); // ��l�Ʈ����ô��ܤ奻
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.R))
        {
                      
            if (uiPanel != null)
            {
                uiPanel.SetActive(!uiPanel.activeSelf); // ����UI���O��ܪ��A
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
                promptTextUI.SetActive(true); // ��ܴ��ܤ奻
            }
        }

        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {           
            if (promptTextUI != null)
            {
                promptTextUI.SetActive(false); // ���ô��ܤ奻
            }
        }
    }

    
}
