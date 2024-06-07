using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAction : MonoBehaviour
{
    public Transform Player;
    public float followSpeed = 5f;
    public float stoppingDistance = 3f;

    public Animator animator;
    float angel;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isTurn", true);
    }

    void Update()
    {
        NPCMove();
    }
    public void NPCMove()
    {
        if (Player != null)
        {
            float distanceToLeader = Vector2.Distance(transform.position, Player.position);

            if (distanceToLeader > stoppingDistance)
            {
                Vector2 direction = (Player.position - transform.position).normalized;
                angel = Vector2.SignedAngle(direction, transform.right);
                //Debug.Log(angel);
                animator.SetBool("isMove", true);
                animator.SetFloat("angel", angel);
                transform.position = Vector2.MoveTowards(transform.position, Player.position, followSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isMove", false);
            }
        }
    }
}

