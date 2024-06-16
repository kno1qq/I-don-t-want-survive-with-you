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
    private bool bossShown = false;//�˴�Boss�O�_�w�ͦ�017

    public GameObject weaponHolder;
    public Animator weaponHolderAni;
    Vector3 target, direction; //�ƹ����ؼ� ��V
    Quaternion targetRotation;

    bool isWalkAudioDone = true;

    public GameObject DieScreen;//���`�e��
    public GameObject GridPanel;//���t���ꤶ��
    public GameObject T;//�ɶ� 
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
                GameManager.instance.OpenTipPanel("���U�ƹ��������");
                break;
            case "bossShow": //�i�JBoss�ͦ���017
                if (!bossShown)
                {
                    GameManager.instance.OnPlayerEnterBossShowBox();
                    GameObject triggerbox = GameObject.Find("TriggerBox");
                    triggerbox.SetActive(false);
                    bossShown = true;
                }
                break;
            case "Door"://�I��x�ư�a�j���ɶ}��017
                GameObject tilmapObject = GameObject.Find("Door");
                tilmapObject.SetActive(false);
                StartCoroutine(GameManager.instance.showTip("�A���}�F�x�ư�a�j��"));
                break;
            case "fireball"://�Q���y�{�즩��017
                DecreaseHealth(10);
                break;
            default:
                break;

        }
    }
    void Walk()//����
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
        //�������ʵe
        if (Input.GetButtonDown("Fire1"))
        {
            target = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            direction = target - transform.position;
            targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
            weaponHolder.transform.rotation = targetRotation;
            weaponHolderAni.SetTrigger("attack");
            GameManager.instance.audioManager.Play(2, "seAttack", false);//��������017
        }
    }
    /* �g�W�[��q���ثe�Τ���
    public void IncreaseHealth(float amont)
    {
        SetHealth(currentHp + amont);
    }
    */
    public void SetHealth(float health)//�]�w��q
    {
        currentHp = Mathf.Clamp(health, 0f, maxHp);//��q����0-100
        UpdateHealthBar(); //��s��q

        if (currentHp <= 0)//���`�e��
        {
            isEnabledWalk = false;

            if (DieScreen != null)
            {
                DieScreen.SetActive(true);//���`�e�����}
                GridPanel.SetActive(false);//��������e������
                T.SetActive(false);//�p�ɮɶ�����
            }

        }
    }
    public void DecreaseHealth(float amont)//��֦�q�ϥιϤ��̭���fill amont
    {
        SetHealth(currentHp - amont);
        GameManager.instance.audioManager.Play(3, "seDamage", false);//���孵��017
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
        GameManager.instance.audioManager.Play(4, "seWalk", false);//���a��������017
        yield return new WaitForSeconds(0.3f);
        isWalkAudioDone = true;
    }
    public void CheckScene()
    {
        int GetSceneName = SceneManager.GetActiveScene().buildIndex;

        if (GetSceneName == 4)//Women Buff
        {
            totalTime += Time.deltaTime;//�C����[��q
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
            totalTime += Time.deltaTime;//�C����[��q
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
