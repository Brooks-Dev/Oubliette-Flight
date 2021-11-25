using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game manager is null!");
            }
            return _instance;
        }
    }

    [SerializeField]
    private AudioSource _playerAudioEffects;
    [SerializeField]
    private AudioClip _swingSword, _swingSwordFlame;
    [SerializeField]
    private AudioClip _playerJump, _playerDeath;
    [SerializeField]
    private AudioClip _getGem;
    [SerializeField]
    private AudioClip _buyItem;

    void Awake()
    {
        _instance = this;
    }

    public void SwingSword()
    {
        _playerAudioEffects.clip = _swingSword;
        _playerAudioEffects.time = 0f;
        _playerAudioEffects.Play();
    }

    public void SwingSwordFlame()
    {
        _playerAudioEffects.clip = _swingSwordFlame;
        _playerAudioEffects.time = 0f;
        _playerAudioEffects.Play();
    }

    public void PlayerDied()
    {
        _playerAudioEffects.clip = _playerDeath;
        _playerAudioEffects.time = 0f;
        _playerAudioEffects.Play();
    }

    public void GemPickup()
    {
        _playerAudioEffects.clip = _getGem;
        _playerAudioEffects.time = 0f;
        _playerAudioEffects.Play();
    }

    public void BuyItemFromShop()
    {
        _playerAudioEffects.clip = _buyItem;
        _playerAudioEffects.time = 0f;
        _playerAudioEffects.Play();
    }

    public void PlayerJump()
    {
        _playerAudioEffects.clip = _playerJump;
        _playerAudioEffects.time = 0f;
        _playerAudioEffects.Play();
        StartCoroutine(ShortenJumpSound());
    }

    private IEnumerator ShortenJumpSound()
    {
        yield return new WaitForSeconds(0.3f);
        _playerAudioEffects.Stop();
    }

    public void PlayerLands()
    {
        _playerAudioEffects.clip = _playerJump;
        _playerAudioEffects.time = 0.45f;
        _playerAudioEffects.Play();
    }
}
