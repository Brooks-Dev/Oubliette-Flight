using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _playerVelocity = 3.0f, _jumpVelocity = 4f;
    [SerializeField]
    private float _distToGround = 0.75f;
    private bool _playerGrounded;
    private float _horizontal, _vertical;
    private PlayerAnimation _playerAnim;
    private bool _isJumping = false;

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
    }

    // Update is called once per frame
    void Update()
    {
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
            _horizontal = Input.GetAxisRaw("Horizontal") * _playerVelocity;
            _playerAnim.Move(_horizontal);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Player jumps up
                _vertical += _jumpVelocity;
                StartCoroutine(PlayerJumps());
                _playerAnim.Jumping(_isJumping);
            }

        }

        _rigidbody.velocity = new Vector2(_horizontal, _vertical);
    }

    void Attacks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _playerAnim.Attack();
        }
    }

    IEnumerator PlayerJumps()
    {
        _isJumping = true;
        yield return new WaitForSeconds(0.1f);
        _isJumping = false;
    }
}
