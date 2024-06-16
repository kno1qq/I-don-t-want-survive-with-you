using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss_Status : MonoBehaviour
{
    public float maxHp = 500f;
    public float currentHp;
    private Animator animator;

    public bool isInvulnerable = false;
    public bool isHit = false;
    private bool isHelfHP=false;
    public Image hpImg;
    public Image hpEffectImg;
    public float buffTime = 0.5f;

    public string sceneToLoad;
    void Start()
    {
        animator = GetComponent<Animator>();                 
    }
   
   public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="nife")
        {
            DecreaseHealth(10);
        }
       
    }
   

    public void DecreaseHealth(float amont)//減少血量
    {       
        if (isInvulnerable)
            return;

        if (isHit)
            return;
      
        animator.SetTrigger("Hit");
         GameManager.instance.audioManager.Play(5,"seBossHit",false);
        SetHealth(currentHp - amont);
        if (currentHp <= 250&&!isHelfHP)
        {
            isHelfHP=true;
            animator.SetBool("isRange2",true);
            StartCoroutine(GameManager.instance.showTip("可惡的凡人!!!!受死吧!!!")); 
        }
        if(currentHp<=0)
        {
            Die();
        }
    }

     public void Die()
    {    
        StartCoroutine(GameManager.instance.showTip("可惡的凡人!!!!我會復仇的!!!"));    
        animator.SetTrigger("Die");    
        Camerashake.Instance.ShakeCamera(5f,5f);   
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        StartCoroutine(TransitionCoroutine(sceneToLoad));
        //Destroy(gameObject);
    }

    public void SetHealth(float health)//設定血量
    {
        currentHp = Mathf.Clamp(health, 0f, maxHp);//目前血量
        UpdateHealthBar(); //更新血量
       
    }

     private void UpdateHealthBar()
    {
        hpImg.fillAmount = currentHp / maxHp; //fillamount 0~1            
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
    public IEnumerator TransitionCoroutine(string newSceneName)
    {
        yield return new WaitForSeconds(3);
        yield return StartCoroutine(ScreenFader.Instance.FadeScreenOut());

        SceneManager.LoadScene(newSceneName);

        yield return StartCoroutine(ScreenFader.Instance.FadeScreenIn());
    }
}
