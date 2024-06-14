using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructibleGress : LivingEntity
{
    public override void TakeDamage(float damage)
    {
        if (damage >= Health && !IsDead)
        {
            OnDeath += () =>
            {
                Debug.Log("wall is broke!!!");
            };
        }
        base.TakeDamage(damage);
    }
}
