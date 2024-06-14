using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoss_Attack : MonoBehaviour
{   
    public Transform firePoint;
    public GameObject fireball;
    public int numberOfFireballs = 8;
    public int numberOfFirerain = 20;
  
    public float minSpawnDelay = 0.5f;
    public float maxSpawnDelay = 3f;
    public float explodeDelay = 2f;

    private Animator animator;


    void Start()
    {
        animator = fireball.GetComponent<Animator>();
    }
   
    public void FireballSmash()
    {
        float angleStep = 360f / numberOfFireballs;
        float angle = 0f;       
        for (int i = 0; i < numberOfFireballs; i++)
        {           
            float fireballDirX = firePoint.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float fireballDirY = firePoint.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 fireballVector = new Vector3(fireballDirX, fireballDirY, 0);
            Vector3 fireballMoveDirection = (fireballVector - firePoint.position).normalized;
          
            GameObject tempFireball = Instantiate(fireball, firePoint.position, firePoint.rotation);
            tempFireball.transform.Rotate(0, 0, angle);

          
            /*Rigidbody2D rb = tempFireball.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(fireballMoveDirection.x, fireballMoveDirection.y) * speed;*/

            angle += angleStep;
        }
        GameManager.instance.audioManager.Play(0,"seBossAttack",false);
    }

    public void FireRain()
    {       
        StartCoroutine(SpawnFireballs());
    }

    private IEnumerator SpawnFireballs()
    {
        for (int i = 0; i < numberOfFirerain; i++)
        {
            float randomX = Random.Range(-52f, 8f);
            float randomY = Random.Range(-10f, 10f);
            Vector3 randomPosition = new Vector3(randomX, randomY, 0f);
            Quaternion rotation = Quaternion.Euler(0f, 0f, -90f);
            
            //�ͦ����B
            GameObject newFireball = Instantiate(fireball, randomPosition, rotation);

            //������B�z���ɶ�
            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(spawnDelay);


            animator.SetTrigger("Explode");            
          
        }
    }
 

}
