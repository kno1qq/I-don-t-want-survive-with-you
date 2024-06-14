using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    bool isdone;
   
    public GameObject Button;
    // Start is called before the first frame update
    void Start()
    {
        isdone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.R) && !isdone)
        {
            GameManager.instance.openDialog(1);
            Button.SetActive(false);
            isdone = true;
        }
        if (Button.activeSelf && Input.GetKeyDown(KeyCode.R) && isdone)
        {
            GameManager.instance.openDialog(2);
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
