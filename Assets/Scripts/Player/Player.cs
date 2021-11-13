using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamagable
{
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _playerVelocity = 3.0f, _jumpVelocity = 4f;
    [SerializeField]
    private float _distToGround = 0.75f;
    [SerializeField]
    public int diamonds;
    private bool _playerGrounded;
    private float _horizontal, _vertical;
    private PlayerAnimation _playerAnim;
    private bool _isJumping = false;
    public bool InShop;
    [SerializeField]
    public int Health { get; set; }
    public bool PlayerIsDead;

    // Start is called before the first frame update
    void Start()
    {
        _playerAnim = GetComponent<PlayerAnimation>();
        if (_playerAnim == null)
        {
            Debug.LogError("Player animation is null in player!");
        }
        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody == false)
        {
            Debug.LogError("Rigidbody on player is null!");
        }
        Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerIsDead == true) return;
        _playerGrounded = Physics2D.Raycast(transform.position, Vector2.down, _distToGround, LayerMask.GetMask("Ground"));
        Attacks();
        Movement();
    }

    void Movement()
    {
        _vertical = _rigidbody.velocity.y;
        if (_playerGrounded == true)
        {
            Debug.DrawRay(transform.position, Vector2.down * _distToGround, Color.yellow);
            if (_isJumping == false)
            { 
                _playerAnim.Jumping(_isJumping);
            }
            //player can move left and right
            if (Application.isEditor)
            {
                _horizontal = Input.GetAxisRaw("Horizontal") * _playerVelocity;
            }
            else
            {
                _horizontal = CrossPlatformInputManager.GetAxis("Horizontal") * _playerVelocity;
            }
            _playerAnim.Move(_horizontal);

            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //Player jumps up
                    _vertical += _jumpVelocity;
                    StartCoroutine(PlayerJumps());
                    _playerAnim.Jumping(_isJumping);
                }
            }
            else
            {
                if (CrossPlatformInputManager.GetButtonDown("B_Button"))
                {
                    //Player jumps up
                    _vertical += _jumpVelocity;
                    StartCoroutine(PlayerJumps());
                    _playerAnim.Jumping(_isJumping);
                }
            }


        }

        _rigidbody.velocity = new Vector2(_horizontal, _vertical);
    }

    void Attacks()
    {
        if (InShop == false)
        {
            if (Application.isEditor)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _playerAnim.Attack();
                }
            }
            else
            {
                if (CrossPlatformInputManager.GetButtonDown("A_Button"))
                {
                    _playerAnim.Attack();
                }
            }
        }
    }

    IEnumerator PlayerJumps()
    {
        _isJumping = true;
        yield return new WaitForSeconds(0.1f);
        _isJumping = false;
    }

    public void Damage()
    {
        if (PlayerIsDead == true) return;
        Health--;
        if (Health <= 0)
        {
            Debug.Log("Player dies");
            _playerAnim.PlayerDies();
            PlayerIsDead = true;
        }
        UIManager.Instance.UpdateLives(Health);
    }

    public void GetDiamond()
    {
        diamonds++;
        UIManager.Instance.UpdateGemCount(diamonds);
    }
}
