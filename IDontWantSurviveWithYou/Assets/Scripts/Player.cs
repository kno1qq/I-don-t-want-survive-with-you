using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

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

    public GameObject weaponHolder;
    public Animator weaponHolderAni;
    Vector3 target, direction; //滑鼠的目標 方向
    Quaternion targetRotation;
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        _transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }
   
    void Update()
    {
        Walk();
        attack();
    }
    private void FixedUpdate()
    {
        //計算移動方向
        if (isEnabledWalk && !GameManager.instance.isTalk)
        {
            moveDirection = new Vector2(moveX, moveY).normalized;
            rb.velocity = moveDirection * speed;
        }
        else
            rb.velocity = Vector2.zero;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "chat" && GameManager.instance.GameState == 1) 
        {
            GameManager.instance.openDialog(1);
            GameManager.instance.GameState = 2;
        }
        if (collision.tag == "GoInShelter")
        {
            SceneManager.LoadScene("Shelter");
        }
    
    }
    void Walk()
    {
        if (isEnabledWalk && !GameManager.instance.isTalk)
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
        //攻擊的動畫
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
