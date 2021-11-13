using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    const string adUnitID = "OF_Shop_Ad";
    const string gameID = "4447771";
    private bool _testMode = false;
    private bool _adReady;

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, _testMode);
    }

    public void DisplayVideoAd()
    {
        if (_adReady == true)
        {
            Advertisement.Show(adUnitID);
        }
    }

    public void OnUnityAdsReady(string adUnitID)
    {
        _adReady = true;
    }

    public void OnUnityAdsDidFinish(string adUnitID, ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Player gets 100G");
                break;
            case ShowResult.Skipped:
                Debug.Log("No gems for you");
                break;
            case ShowResult.Failed:
                Debug.Log("Ad failed.");
                break;
            default:
                break;
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log(message);
    }

    public void OnUnityAdsDidStart(string adUnitID)
    {
        //optional actions on triggering an add
    }
}
