using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;

    protected Vector3 target;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;
    protected bool IsHit;
    protected Transform player;

    public virtual void Init()
    {
        player = GameObject.Find("Player").GetComponent<Player>().transform; 
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        target = pointA.position;
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        float distToPlayer = Vector3.Distance(player.position, transform.position);
        if (distToPlayer > 2.0f)
        {
            IsHit = false;
            anim.SetBool("InCombat", false);
        }
        else
        {
            spriteRenderer.flipX = false;
            Vector3 direction = (player.position - transform.position);
            if (direction.x < 0f)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            anim.SetBool("InCombat", true);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return;
        }
        MoveMonster();
    }

    public virtual void MoveMonster()
    {
        if (IsHit == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        if (Vector3.Distance(target, pointA.position) == 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            target = pointB.position;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            target = pointA.position;
            anim.SetTrigger("Idle");
        }
    }
    public virtual void Attack()
    {
        
    }

}
