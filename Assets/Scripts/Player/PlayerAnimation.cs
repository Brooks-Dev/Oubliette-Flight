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
        }
        else if (move < 0)
        {
            _renderer.flipX = true;
            _renderer.transform.localPosition = new Vector3(-0.09f, 0, 0);
        }
    }

    public void Attack(bool grounded)
    {
        if (GameManager.Instance.FlamingSword == true)
        {
            _animator.SetBool("Flaming_Sword", true);
        }
        else
        {
            _animator.SetBool("Flaming_Sword", false);
        }
        if (grounded == true)
        {
            if (_renderer.flipX == true)
            {
                _swordRenderer.flipX = true;
                _swordRenderer.flipY = true;
                _swordRenderer.transform.localPosition = new Vector3(0.09f, 0, 0);
                _swordRenderer.transform.localEulerAngles = new Vector3(250f, 50f, -150f);
            }
            else
            {
                _swordRenderer.flipX = false;
                _swordRenderer.flipY = false;
                _swordRenderer.transform.localPosition = new Vector3(-0.09f, 0, 0);
                _swordRenderer.transform.localEulerAngles = new Vector3(75f, 50f, -150f);
            }
            _swordAnimator.SetBool("Jump", false);
        }
        else
        {
            if (_renderer.flipX == true)
            {
                if (_renderer.flipX == true)
                {
                    _swordRenderer.flipX = false;
                    _swordRenderer.flipY = true;
                    _swordRenderer.transform.localPosition = new Vector3(0.09f, 0.2f, 0);
                    _swordRenderer.transform.localEulerAngles = new Vector3(110f, 50f, -84f);
                }
            }
            else
            {
                if (_renderer.flipX == false)
                {
                    _swordRenderer.flipX = true;
                    _swordRenderer.flipY = false;
                    _swordRenderer.transform.localPosition = new Vector3(-0.09f, 0.2f, 0);
                    _swordRenderer.transform.localEulerAngles = new Vector3(66f, 50f, -84f);
                }
            }
            _swordAnimator.SetBool("Jump", true);
        }
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (GameManager.Instance.FlamingSword == true)
            {
                AudioManager.Instance.SwingSwordFlame();
            }
            else
            {
                AudioManager.Instance.SwingSword();
            }
            _swordAnimator.SetTrigger("SwordAnimation");
            _animator.SetTrigger("Attack");
        }
    }

    public void Jumping(bool isJumping)
    {
        if(_animator.GetBool("Jump") == true)
        {
            AudioManager.Instance.PlayerLands();
        }
        _animator.SetBool("Jump", isJumping);
    }

    public void PlayerDies()
    {
        _animator.SetTrigger("Death");
        AudioManager.Instance.PlayerDied();
    }
}
