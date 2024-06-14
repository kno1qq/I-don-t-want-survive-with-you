using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M3FOOD : MonoBehaviour
{
    bool isdone;
    public GameObject UIPanel;
    public GameObject Button;
    public GameObject endUIPanel;
    public GameObject specialObject;
    void Start()
    {
        isdone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.R) && !isdone)
        {
            UIPanel.SetActive(true);
            Button.SetActive(false);
            isdone = true;
        }
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.R) && isdone)
        {
            GameManager.instance.openDialog(4);
            Destroy(endUIPanel);
            specialObject.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Button.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Button.SetActive(false);
        }
    }
}
