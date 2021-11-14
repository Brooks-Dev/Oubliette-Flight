using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField]
    private const string _adUnitID = "OF_Shop_Ad";
    [SerializeField]
    private Button _shopButton;
    private Player _player;

    void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _shopButton.interactable = false;
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitID);
        Advertisement.Load(_adUnitID, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitID)
    {
        Debug.Log("Ad Loaded: " + adUnitID);
        if (adUnitID.Equals(_adUnitID))
        {
            // Configure the button to call the ShowAd() method when clicked:
            _shopButton.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            _shopButton.interactable = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitID, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitID}: {error} - {message}");
    }

    public void ShowAd()
    {
        // Disable the button: 
        _shopButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitID, this);
    }

    public void OnUnityAdsShowFailure(string adUnitID, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitID}: {error} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitID)
    {
        Debug.Log("Ad started");
    }

    public void OnUnityAdsShowClick(string adUnitID)
    {
        Debug.Log("Ad clicked");
    }

    public void OnUnityAdsShowComplete(string adUnitID, UnityAdsShowCompletionState showCompletionState)
    {
        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.COMPLETED:
                Debug.Log("Player gets 100G");
                _player.GetDiamond(100);
                StartCoroutine(ReloadAd());
                break;
            case UnityAdsShowCompletionState.SKIPPED:
                Debug.Log("No gems for you");
                break;
            case UnityAdsShowCompletionState.UNKNOWN:
                Debug.Log("Ad failed.");
                break;
            default:
                break;
        }
    }

    private IEnumerator ReloadAd()
    {
        yield return new WaitForSeconds(20f);
        LoadAd();
    }
}
