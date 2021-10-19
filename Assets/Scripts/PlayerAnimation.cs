using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
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

    public void Jumping(bool isJumping)
    {
        _animator.SetBool("Jump", isJumping);
    }
}
