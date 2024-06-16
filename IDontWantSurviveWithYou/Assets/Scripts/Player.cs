using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : LivingEntity
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

    public Image hpImg;
    public Image hpEffectImg;

    public float maxHp = 100f;
    public float currentHp;
    public float buffTime = 0.5f;
    private Coroutine updateCoroutine;
    private bool bossShown = false;//檢測Boss是否已生成017

    public GameObject weaponHolder;
    public Animator weaponHolderAni;
    Vector3 target, direction; //滑鼠的目標 方向
    Quaternion targetRotation;

    bool isWalkAudioDone = true;

    public GameObject DieScreen;//死亡畫面
    public GameObject GridPanel;//分配物資介面
    public GameObject T;//時間 
    public float M3Buff = 0f;
    private float totalTime;

    public string sceneToLoad;
    void Start()
    {
        _transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        currentHp = maxHp;
        UpdateHealthBar();
    }
   
    void Update()
    {
        Walk();
        attack();
        CheckScene();
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
        switch (collision.tag)
        {
            case "chat":
                GameManager.instance.openDialog(1);
                GameManager.instance.GameState = 2;
                break;
            case "chatB":
                GameManager.instance.openDialog(2);
                GameManager.instance.GameState = 3;
                break;
            case "GoInShelter":
                StartCoroutine(TransitionCoroutine(sceneToLoad));
                break;
            case "nife":
                GameManager.instance.openDialog(1);
                weaponHolder.SetActive(true);
                GameManager.instance.OpenTipPanel("按下滑鼠左鍵攻擊");
                break;
            case "bossShow": //進入Boss生成區017
                if (!bossShown)
                {
                    GameManager.instance.OnPlayerEnterBossShowBox();
                    GameObject triggerbox = GameObject.Find("TriggerBox");
                    triggerbox.SetActive(false);
                    bossShown = true;
                }
                break;
            case "Door"://碰到軍事基地大門時開門017
                GameObject tilmapObject = GameObject.Find("Door");
                tilmapObject.SetActive(false);
                StartCoroutine(GameManager.instance.showTip("你打開了軍事基地大門"));
                break;
            case "fireball"://被火球砸到扣血017
                DecreaseHealth(10);
                break;
            default:
                break;

        }
    }
    void Walk()//移動
    {
        
        if (isEnabledWalk)
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");

            playAni.SetFloat("Horizontal", moveX);
            playAni.SetFloat("Vertical", moveY);
            playAni.SetFloat("Speed", moveDirection.sqrMagnitude);

            bool isMoving = (moveX != 0 || moveY != 0);
            if (isWalkAudioDone && isMoving)
            {
                StartCoroutine(walkAudio());
            }
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
            GameManager.instance.audioManager.Play(2, "seAttack", false);//攻擊音效017
        }
    }
    /* 寫增加血量的目前用不到
    public void IncreaseHealth(float amont)
    {
        SetHealth(currentHp + amont);
    }
    */
    public void SetHealth(float health)//設定血量
    {
        currentHp = Mathf.Clamp(health, 0f, maxHp);//血量介於0-100
        UpdateHealthBar(); //更新血量

        if (currentHp <= 0)//死亡畫面
        {
            isEnabledWalk = false;

            if (DieScreen != null)
            {
                DieScreen.SetActive(true);//死亡畫面打開
                GridPanel.SetActive(false);//收集物資畫面關閉
                T.SetActive(false);//計時時間關閉
            }

        }
    }
    public void DecreaseHealth(float amont)//減少血量使用圖片裡面的fill amont
    {
        SetHealth(currentHp - amont);
        GameManager.instance.audioManager.Play(3, "seDamage", false);//扣血音效017
    }
    private void UpdateHealthBar()
    {
        hpImg.fillAmount = currentHp / maxHp; //fillamount 0~1
        if (updateCoroutine != null)
        {
            StopCoroutine(updateCoroutine);
        }
        updateCoroutine = StartCoroutine(UpdateHpEffect());
    }
    private IEnumerator UpdateHpEffect()
    {
        float effectLength = hpEffectImg.fillAmount - hpImg.fillAmount;
        float elaspsedTIme = 0f;

        while (elaspsedTIme < buffTime && effectLength != 0)
        {
            elaspsedTIme += Time.deltaTime;
            hpEffectImg.fillAmount = Mathf.Lerp(hpImg.fillAmount + effectLength, hpImg.fillAmount, elaspsedTIme / buffTime);
            yield return null;
        }
    }
    private IEnumerator walkAudio()
    {
        isWalkAudioDone = false;
        GameManager.instance.audioManager.Play(4, "seWalk", false);//玩家走路音效017
        yield return new WaitForSeconds(0.3f);
        isWalkAudioDone = true;
    }
    public void CheckScene()
    {
        int GetSceneName = SceneManager.GetActiveScene().buildIndex;

        if (GetSceneName == 4)//Women Buff
        {
            totalTime += Time.deltaTime;//每五秒加血量
            if (totalTime >= 5)
            {
                if (currentHp > 0 && currentHp < maxHp)
                    currentHp += 5;
                UpdateHealthBar();
                totalTime = 0;
            }
            speed = 3;
        }
        if (GetSceneName == 5)//M3 Buff
        {
            M3Buff = 5;
        }
        if (GetSceneName == 6)//Old Men Buff
        {
            speed = 10;
        }
        if (GetSceneName == 14)//Old Men Buff
        {
            totalTime += Time.deltaTime;//每五秒加血量
            if (totalTime >= 5)
            {
                if (currentHp > 0 && currentHp < maxHp)
                    currentHp += 5;
                UpdateHealthBar();
                totalTime = 0;
            }
            M3Buff = 5;
            speed = 10;
        }

    }
    public IEnumerator TransitionCoroutine(string newSceneName)
    {
        yield return StartCoroutine(ScreenFader.Instance.FadeScreenOut());

        SceneManager.LoadScene(newSceneName);

        yield return StartCoroutine(ScreenFader.Instance.FadeScreenIn());
    }
}
