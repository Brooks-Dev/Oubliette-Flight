using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    [SerializeField]
    private Image _selectionImage;
    [SerializeField]
    private Text _playerHUDGemCountText;
    [SerializeField]
    private Image[] _lifeUnits;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is null");
            }
            return _instance;
        }
    }
    
    public Text playerGemCountText;

    private void Awake()
    {
        _instance = this;
        _selectionImage.gameObject.SetActive(false);
        _lifeUnits = GameObject.Find("Life_Units").GetComponentsInChildren<Image>();
    }

    public void OpenShop(int gems)
    {
        playerGemCountText.text = gems.ToString() + " Gems";
    }

    public void UpdateShopSection(bool open, int yPos)
    {
        _selectionImage.gameObject.SetActive(open);
        _selectionImage.rectTransform.anchoredPosition = new Vector2(_selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemCount(int gems)
    {
        _playerHUDGemCountText.text = gems.ToString();
    }

    public void UpdateLives(int health)
    {
        _lifeUnits = GameObject.Find("Life_Units").GetComponentsInChildren<Image>();
        for (int i = 0; i <= health; i++)
        {
            if (i <= health - 1)
            {
                _lifeUnits[i].enabled = true;
            }
            else
            {
                _lifeUnits[i].enabled = false;
            }
        }
    }
}
