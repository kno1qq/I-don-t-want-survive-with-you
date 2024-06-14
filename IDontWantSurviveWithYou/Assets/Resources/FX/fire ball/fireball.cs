using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;    
 

    public float xMin = -52f; 
    public float xMax = 8f; 
    public float yMin = -48f; 
    public float yMax = 6f; 

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right*speed;
        
    }

    void Update()
    {
        CheckBounds();
    }

    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {         
            Destroy(gameObject);         
        }
    }

    void CheckBounds()
    {      
        if (transform.position.x < xMin || transform.position.x > xMax ||
            transform.position.y < yMin || transform.position.y > yMax)
        {
          
            Destroy(gameObject);
        }
    }
}
