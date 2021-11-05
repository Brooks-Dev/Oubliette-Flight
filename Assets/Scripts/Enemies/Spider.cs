using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamagable
{
    public int Health { get; set; }
    [SerializeField]
    private GameObject _acid;
    private Vector3 _acidTrans;
    public override void Init()
    {
        base.Init();
        Health = base.health;
        _acidTrans = this.transform.position;
        _acidTrans += new Vector3(-0.5f, 0, 0);
    }

    public override void MoveMonster()
    {
        //no movement
    }
    public void Damage()
    {
        if (IsDead == true)
        {
            return;
        }
        Health--;
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
        }
        anim.SetTrigger("Hit");
        anim.SetBool("InCombat", true);
    }

    public void Attack()
    {
        if (_acid != null)
        { 
            Instantiate(_acid, _acidTrans, Quaternion.identity); 
        }
    }
}
