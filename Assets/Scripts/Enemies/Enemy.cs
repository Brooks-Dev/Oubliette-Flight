using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;
    [SerializeField]
    protected Transform target;
    [SerializeField]
    protected Transform newTarget;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;
    public virtual void Attack()
    {
        Debug.Log("Base attack for " + this.gameObject.name);
    }

    public abstract void Update();
}
