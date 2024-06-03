using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startHealth;
    protected float Health { get; private set; }
    protected bool IsDead;

    public event Action OnDeath;
    
    protected virtual void Start()
    {
        Health = startHealth;
    }
    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health > 0 || IsDead) return;

        IsDead = true;
        OnDeath?.Invoke();

        Destroy(gameObject);
    }
}
