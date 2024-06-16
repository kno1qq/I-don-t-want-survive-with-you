﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class player2 : MonoBehaviour
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

    public GameObject weaponHolder;
    public Animator weaponHolderAni;
    Vector3 target, direction; //�ƹ����ؼ� ��V
    Quaternion targetRotation;
    void Start()
    {
        _transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        GameManager.instance.openDialog(0);
        //GameManager.instance.OpenTipPanel("");

    }

    void Update()
    {
        Walk();
        attack();
    }
    private void FixedUpdate()
    {

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
    void Walk()
    {
        if (isEnabledWalk)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");

            playAni.SetFloat("Horizontal", moveX);
            playAni.SetFloat("Vertical", moveY);
            playAni.SetFloat("Speed", moveDirection.sqrMagnitude);
        }
    }
    void attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            target = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            direction = target - transform.position;
            targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
            weaponHolder.transform.rotation = targetRotation;
            weaponHolderAni.SetTrigger("attack");
        }
    }
}