using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    private void Start()
    {
        if (_audioSource == null)
        {
            Debug.LogError("Audio source in main menu is null!");
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Game");
        _audioSource.Play();
    }

    public void QuitButton()
    {
        Application.Quit();
        _audioSource.Play();
    }
}
