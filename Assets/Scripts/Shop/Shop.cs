using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                UIManager.Instance.OpenShop(player.diamonds);
            }

            _shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Shop exited"); 
        _shopPanel.SetActive(false);
    }

    public void SelectItem(int item)
    {
        //0 flame sword, 1 boots of flight, 2 key to castle
        Debug.Log("SelectItem(" + item + ")");
        switch (item)
        {
            case 0:
                UIManager.Instance.UpdateShopSection(79);
                break;
            case 1:
                UIManager.Instance.UpdateShopSection(-30);
                break;
            case 2:
                UIManager.Instance.UpdateShopSection(-129);
                break;
            default:
                break;
        }
    }
}
