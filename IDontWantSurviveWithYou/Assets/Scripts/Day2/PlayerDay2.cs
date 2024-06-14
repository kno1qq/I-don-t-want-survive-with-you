using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerDay2 : MonoBehaviour
{
    public float speed = 5;
    public float moveX, moveY;
    Rigidbody2D rb;
    Transform _transform;
    Vector2 moveDirection;
    public Animator playAni;    
    public bool isEnabledWalk = true;

    public CinemachineVirtualCamera vCam;

    public GameObject dialogPanel;

    public GameObject weaponHolder;
    public Animator weaponHolderAni;

    public GameObject uiPanel;
    private int rPressCount = 0;
    Vector3 target, direction; 
    Quaternion targetRotation;

    bool isWalkAudioDone = true;
    void Start()
    {
        _transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        if (GameManager.instance != null)
        {
            GameManager.instance.openDialog(0);
            GameManager.instance.GameState = 1;
        }
        else
        {
            Debug.LogError("GameManager instance is null. Make sure GameManager is properly initialized.");
        }

        if (uiPanel != null)
        {
            uiPanel.SetActive(false);  // 初始化时隐藏 UI 面板
        }


    }

    void Update()
    {
        Walk();
        attack();
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Day2")
        {
            if (GameManager.instance.GameState == 1 && !GameManager.instance.isTalk)
            {
                uiPanel.SetActive(true);
            }
        }
        if (sceneName == "Day3.5")
        {
            if (GameManager.instance.GameState == 1 && !GameManager.instance.isTalk)
            {
                GameManager.instance.OpenTipPanel("和前女友一起出門");
                GameManager.instance.GameState = 2;
            }
        }
        
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
        bool isMoving = (moveX != 0 || moveY != 0);
        if (isWalkAudioDone && isMoving)
        {
            StartCoroutine(walkAudio());
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
    private IEnumerator walkAudio()
    {
        isWalkAudioDone = false;
        GameManager.instance.audioManager.Play(4, "seWalk", false);//玩家走路音效017
        yield return new WaitForSeconds(0.3f);
        isWalkAudioDone = true;
    }
}

