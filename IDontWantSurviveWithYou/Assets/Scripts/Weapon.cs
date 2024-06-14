using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public LayerMask whatToHit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("10");
        IDamageable damageable = collision.transform.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(10);
            PopupText.Create(collision.transform.position, Random.Range(1, 100), Random.Range(0, 100) < 30);
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("10");
        IDamageable damageable = collision.transform.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(10);
            PopupText.Create(collision.transform.position, Random.Range(1, 100), Random.Range(0, 100) < 30);
        }
    }
    */
}
