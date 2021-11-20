using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
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

    public bool HasKeyCastle { get; set; }
    public bool FlamingSword { get; set; }
    public bool BootsOfFlight { get; set; }
    public Player Player { get; private set; }

    private void Awake()
    {
        _instance = this;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void GetDiamond(int count)
    {
        Player.diamonds += count;
        UIManager.Instance.UpdateGemCount(Player.diamonds);
    }
}