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

    public bool isShowedTip = false;
    public bool isShowedDialog = false;
    public bool isEnabledWalk = true;

    public CinemachineVirtualCamera vCam;
    //
    public GameObject dialogPanel;
    void Start()
    {
        _transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        dialogPanel.SetActive(true);
    }
   
    void Update()
    {
        //獲取玩家輸入
        if (isEnabledWalk)
        {
            moveX = Input.GetAxis("Horizontal");
            moveY = Input.GetAxis("Vertical");

            playAni.SetFloat("Horizontal", moveX);
            playAni.SetFloat("Vertical", moveY);
            playAni.SetFloat("Speed", moveDirection.sqrMagnitude);
        }
        //顯示TIP
        if (GameManager.instance.dialogPart == 1 && !isShowedTip)
        {
            StartCoroutine(showTip("使用鍵盤WASD移動"));
            isShowedTip = true;
        }

        if (GameManager.instance.dialogPart == 3)
        {
            vCam.m_Lens.OrthographicSize = 3;
            dialogPanel.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        //計算移動方向
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
        if (collision.tag == "chat" && !isShowedDialog) 
        {
            dialogPanel.SetActive(true);
            isShowedDialog = true;
        }
    }
    IEnumerator showTip(string tipText)
    {
        GameManager.instance.OpenTipPanel(tipText);
        yield return new WaitForSeconds(3);
        GameManager.instance.CloseTipPanel();
    }
}
