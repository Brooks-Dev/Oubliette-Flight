using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamagable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public void Damage(int pain)
    {
        if (IsDead == true)
        {
            return;
        }
        Health -= pain;
        IsHit = true;
        if (Health <= 0)
        {
            IsDead = true;
            anim.SetTrigger("Death");
            for (int i = 0; i < gems; i++)
            {
                var gem = Instantiate(diamond, this.transform.position, Quaternion.identity);
                gem.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f, 2f), 1f);
            }
            DeathAudio();
        }
        anim.SetTrigger("Hit");
        anim.SetBool("InCombat", true);
    }
}
