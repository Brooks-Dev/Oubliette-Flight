using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _renderer;
    private Animator _swordAnimator;
    private SpriteRenderer _swordRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _swordAnimator = GameObject.Find("Sword_Arc").GetComponent<Animator>();
        if (_swordAnimator == null)
        {
            Debug.LogError("Sword animator in PlayerAnimation is null!");
        }
        _swordRenderer = GameObject.Find("Sword_Arc").GetComponent<SpriteRenderer>();
        if (_swordRenderer == null)
        {
            Debug.LogError("Sword renderer in player animation is null!");
        }
        _animator = GetComponentInChildren<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator in Player Anmation is null!");
        }
        _renderer = GetComponentInChildren<SpriteRenderer>();
        {
            if (_renderer == null)
            {
                Debug.LogError("Sprite renderer is null in player animation!");
            }
        }
    }

    public void Move(float move)
    {
        _animator.SetFloat("Move", Mathf.Abs(move));
        if (move > 0)
        {
            _renderer.flipX = false;
            _renderer.transform.localPosition = new Vector3(0.09f, 0, 0);
            _swordRenderer.flipX = false;
            _swordRenderer.flipY = false;
            _swordRenderer.transform.localEulerAngles = new Vector3(66f, 50f, -84f);
        }
        else if (move < 0)
        {
            _renderer.flipX = true;
            _renderer.transform.localPosition = new Vector3(-0.09f, 0, 0);
            _swordRenderer.flipX = true;
            _swordRenderer.flipY = true;
            _swordRenderer.transform.localEulerAngles = new Vector3(110f, 50f, -84f);
        }
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
        _swordAnimator.SetTrigger("SwordAnimation");
    }

    public void Jumping(bool isJumping)
    {
        _animator.SetBool("Jump", isJumping);
    }
}
