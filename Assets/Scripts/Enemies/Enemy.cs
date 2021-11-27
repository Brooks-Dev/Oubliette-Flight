using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    protected AudioSource _audio;
    [SerializeField]
    protected AudioClip _moveClip, _deathClip, _attackClip;
    protected bool _movePlaying, _attackPlaying;

    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;
    [SerializeField]
    protected GameObject diamond;
    [SerializeField]
    protected float attackRange;
    protected bool IsDead;
    protected Vector3 currentScale, flipScale;

    protected Vector3 target;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;
    protected bool IsHit;
    protected Player player;

    public virtual void Init()
    {
        player = GameObject.Find("Player").GetComponent<Player>(); 
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _audio = GetComponent<AudioSource>();
        target = pointA.position;
        currentScale = spriteRenderer.transform.localScale;
        flipScale = currentScale;
        flipScale.x *= -1.0f;
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (IsDead == true) return;
        float distToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distToPlayer > attackRange || player.PlayerIsDead == true)
        {
            IsHit = false;
            anim.SetBool("InCombat", false);
            StopAttackAudio();
        }
        else
        {
            if (IsDead == false && player.PlayerIsDead == false)
            {
                Vector3 direction = (player.transform.position - transform.position);
                if (direction.x < 0f)
                {
                    spriteRenderer.transform.localScale = flipScale;
                }
                else
                {
                    spriteRenderer.transform.localScale = currentScale;
                }
                anim.SetBool("InCombat", true);
                AttackAudio();
            }
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
            MoveAudio();
        }

        if (Vector3.Distance(target, pointA.position) == 0)
        {
            spriteRenderer.transform.localScale = flipScale;
        }
        else
        {
            spriteRenderer.transform.localScale = currentScale;
        }

        if (transform.position == pointA.position)
        {
            target = pointB.position;
            anim.SetTrigger("Idle");
            IdleAudio();
        }
        else if (transform.position == pointB.position)
        {
            target = pointA.position;
            anim.SetTrigger("Idle");
            IdleAudio();
        }
    }

    public virtual void AttackAudio()
    {
        Debug.Log("Moss attack");
        if (_attackPlaying == false)
        {
            _audio.Stop();
            _movePlaying = false;
            _audio.clip = _attackClip;
            _audio.loop = true;
            _audio.Play();
            _attackPlaying = true;
        }
    }

    public virtual void StopAttackAudio()
    {
        if (_attackPlaying == true)
        {
            _audio.loop = false;
            _audio.Stop();
            _attackPlaying = false;
        }
    }
    public virtual void MoveAudio()
    {
        if (_movePlaying == false)
        {
            _audio.Stop();
            _attackPlaying = false;
            _audio.clip = _moveClip;
            _audio.loop = true;
            _audio.Play();
            _movePlaying = true;
        }
    }
    public virtual void IdleAudio()
    {
        _audio.Stop();
        _attackPlaying = false;
        _movePlaying = false;
        _audio.loop = false;
        _movePlaying = false;
    }
    public virtual void DeathAudio()
    {
        _audio.Stop();
        _attackPlaying = false;
        _movePlaying = false;
        _audio.loop = false;
        _audio.clip = _deathClip;
        _audio.Play();
    }
}
