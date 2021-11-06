using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    [SerializeField]
    private Image _selectionImage;

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
    }

    public void OpenShop(int gems)
    {
        playerGemCountText.text = gems.ToString() + " Gems";
    }

    public void UpdateShopSection(int yPos)
    {
        _selectionImage.gameObject.SetActive(true);
        _selectionImage.rectTransform.anchoredPosition = new Vector2(_selectionImage.rectTransform.anchoredPosition.x, yPos);
    }
}
