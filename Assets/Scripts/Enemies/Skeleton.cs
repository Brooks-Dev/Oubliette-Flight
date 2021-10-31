using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamagable
{
    public int Health { get; set; }
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public void Damage()
    {
        Health--;
        IsHit = true;
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
        anim.SetTrigger("Hit");
        anim.SetBool("InCombat", true);
    }
}
