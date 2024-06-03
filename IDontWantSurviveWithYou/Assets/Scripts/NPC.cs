using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject TalkUI;
    public GameObject Button;
    public Animator boss_ani;
    public Animator m3_ani;
    public Animator woman_ani;
    public Animator neighbor_ani;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Button.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            boss_ani.SetBool("isTurn", true);
            m3_ani.SetBool("isTurn", true);
            woman_ani.SetBool("isTurn", true);
            neighbor_ani.SetBool("isTurn", true);

            TalkUI.SetActive(true);
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
