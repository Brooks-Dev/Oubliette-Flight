using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        target = pointA;
        Attack();
    }
    public override void Update()
    {
        MoveGiant();
    }

    private void MoveGiant()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, target.position) < 0.05f)
            {
                target = null;
                anim.SetBool("Walk", false);
            }
        }
        else if (newTarget == null)
        {
            if (Vector2.Distance(transform.position, pointA.position) < 0.05f)
            {
                StartCoroutine(IdleTime(pointB));
            }
            else if (Vector2.Distance(transform.position, pointB.position) < 0.05f)
            {
                StartCoroutine(IdleTime(pointA));
            }
        }
    }

    IEnumerator IdleTime(Transform nextTarget)
    {
        newTarget = nextTarget;
        yield return new WaitForSeconds(5.0f);
        target = newTarget;
        newTarget = null;
        spriteRenderer.flipX = !spriteRenderer.flipX;
        anim.SetBool("Walk", true);
    }
}
