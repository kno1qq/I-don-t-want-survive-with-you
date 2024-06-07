using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster : LivingEntity
{
    public Transform playerTransform;
    Animator animator;
    Vector2 dir;
    float angel;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        monsterMove();
    }
    public override void TakeDamage(float damage)
    {
        if(damage >= Health && !IsDead)
        {
            OnDeath += () =>
            {
                Debug.Log("Monster is dead!!!");
            };
        }
        base.TakeDamage(damage);
    }

    public void monsterMove()
    {
        dir = (playerTransform.position - transform.position).normalized;
        angel = Vector2.SignedAngle(dir, transform.right);
        animator.SetFloat("angel", angel);
    }
}
