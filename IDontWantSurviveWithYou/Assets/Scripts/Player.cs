using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    public float moveX, moveY;
    Rigidbody2D rb;
    Transform _transform;
    Vector2 moveDirection;
    public Animator playAni;

    //public bool isShowedTip = false;
    //public bool isShowedDialog = false;
    public bool isEnabledWalk = true;

    public CinemachineVirtualCamera vCam;
    
    public GameObject dialogPanel;
    void Start()
    {
        _transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        dialogPanel.SetActive(true);
    }
   
    void Update()
    {
        //������a��J
        if (isEnabledWalk)
        {
            moveX = Input.GetAxis("Horizontal");
            moveY = Input.GetAxis("Vertical");

            playAni.SetFloat("Horizontal", moveX);
            playAni.SetFloat("Vertical", moveY);
            playAni.SetFloat("Speed", moveDirection.sqrMagnitude);
        }
        //���TIP
        /*
        if (GameManager.instance.dialogPart == 1 && !PanelManager.instance.isShowedTip)
        {
            StartCoroutine(showTip("�ϥ���LWASD����"));
            PanelManager.instance.isShowedTip = true;
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
            GameManager.instance.openGrid();
        }
        */
    }
    private void FixedUpdate()
    {
        //�p�Ⲿ�ʤ�V
        if (isEnabledWalk)
        {
            moveDirection = new Vector2(moveX, moveY).normalized;
            rb.velocity = moveDirection * speed;
        }
        else
            rb.velocity = Vector2.zero;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "chat" && GameManager.instance.dialogPart == 1) 
        {
            dialogPanel.SetActive(true);
        }
    }
    IEnumerator showTip(string tipText)
    {
        GameManager.instance.OpenTipPanel(tipText);
        yield return new WaitForSeconds(3);
        GameManager.instance.CloseTipPanel();
    }
}
