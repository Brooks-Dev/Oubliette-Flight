using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private GameObject _oubliette, _exit, _deathZone, _gateLocked;
    public int Health { get; set; }
    public bool PlayerIsDead;

    // Start is called before the first frame update
    void Start()
    {
        _playerAnim = GetComponentInChildren<PlayerAnimation>();
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
                    float flight = 1f;
                    if (GameManager.Instance.BootsOfFlight == true) flight = 1.5f;
                    _vertical += flight * _jumpVelocity;
                    StartCoroutine(PlayerJumps());
                    _playerAnim.Jumping(_isJumping);
                }
            }
            else
            {
                if (CrossPlatformInputManager.GetButtonDown("B_Button"))
                {
                    //Player jumps up
                    int flight = 1;
                    if (GameManager.Instance.BootsOfFlight == true) flight = 2;
                    _vertical += flight * _jumpVelocity;
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
                    _playerAnim.Attack(_playerGrounded);
                }
            }
            else
            {
                if (CrossPlatformInputManager.GetButtonDown("A_Button"))
                {
                    _playerAnim.Attack(_playerGrounded);
                }
            }
        }
    }

    IEnumerator PlayerJumps()
    {
        _isJumping = true;
        AudioManager.Instance.PlayerJump();
        yield return new WaitForSeconds(0.1f);
        _isJumping = false;
    }

    public void GetDiamond(int count)
    {
        diamonds += count * 10;
        UIManager.Instance.UpdateGemCount(diamonds);
        AudioManager.Instance.GemPickup();
    }

    public void Damage(int pain)
    {
        if (PlayerIsDead == true) return;
        Health -= pain;
        if (Health <= 0)
        {
            Debug.Log("Player dies");
            _playerAnim.PlayerDies();
            PlayerIsDead = true;
            _rigidbody.velocity = new Vector2(0, _vertical);
            _deathZone.SetActive(true);
            StartCoroutine(ReturnToMainMenu());
        }
        UIManager.Instance.UpdateLives(Health);
    }

    private IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Main_Menu");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Oubliette2") && GameManager.Instance.BootsOfFlight == false)
        {
            _oubliette.SetActive(true);
            StartCoroutine(ReturnToMainMenu());
        }
        else if (other.CompareTag("Oubliette"))
        {
            _oubliette.SetActive(true);
            StartCoroutine(ReturnToMainMenu());
        }
        else if (other.CompareTag("Death Zone"))
        {
            _deathZone.SetActive(true);
            StartCoroutine(ReturnToMainMenu());
        }
        else if (other.CompareTag("Exit"))
        {
            if (GameManager.Instance.HasKeyCastle == true)
            {
                _exit.SetActive(true);
                StartCoroutine(ReturnToMainMenu());
            }
            else
            {
                _gateLocked.SetActive(true);
            }
        }
    }
}
