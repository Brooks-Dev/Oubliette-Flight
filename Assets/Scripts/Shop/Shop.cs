using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopPanel;
    private int _selectedItem;
    private Player _player;
    List<int> ItemCost = new List<int> { 0, 200, 400, 100 };

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<Player>();
            if (_player != null)
            {
                UIManager.Instance.OpenShop(_player.diamonds);
                _player.InShop = true;
                _shopPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _player.InShop = false;
        UIManager.Instance.UpdateShopSection(false, 0);
        _selectedItem = 0;
        _shopPanel.SetActive(false);
    }

    public void SelectItem(int item)
    {
        //0 no item selected, 1 flame sword, 2 boots of flight, 3 key to castle
        _selectedItem = item;
        switch (item)
        {
            case 0:
                break;
            case 1:
                UIManager.Instance.UpdateShopSection(true, 79);
                break;
            case 2:
                UIManager.Instance.UpdateShopSection(true, -30);
                break;
            case 3:
                UIManager.Instance.UpdateShopSection(true, -129);
                break;
            default:
                break;
        }
    }

    public void BuyItem()
    {
        if(_selectedItem > 0)
        {
            if ( _player.diamonds >= ItemCost[_selectedItem])
            {
                switch (_selectedItem)
                {
                    case 3:
                        GameManager.Instance.HasKeyCastle = true;
                        break;
                    case 2:
                        GameManager.Instance.BootsOfFlight = true;
                        break;
                    case 1:
                        GameManager.Instance.FlamingSword = true;
                        break;
                    default:
                        break;
                }
                if (_selectedItem == 3)
                {
                    GameManager.Instance.HasKeyCastle = true;
                }
                Debug.Log("Bought item " + _selectedItem);
                _player.diamonds -= ItemCost[_selectedItem];
                UIManager.Instance.UpdateGemCount(_player.diamonds);
                UIManager.Instance.OpenShop(_player.diamonds);
            }
            else
            {
                _player.InShop = false;
                UIManager.Instance.UpdateShopSection(false, 0);
                _selectedItem = 0;
                _shopPanel.SetActive(false);
            }
        }
    }
}
