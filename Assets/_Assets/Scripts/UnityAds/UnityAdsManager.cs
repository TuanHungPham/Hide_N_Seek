using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsManager : PermanentMonoSingleton<UnityAdsManager>, IUnityAdsInitializationListener
{
    [SerializeField] private UnityRewardAds _unityRewardAds;

    [Header("Initializing Setting")] [SerializeField]
    string _androidGameId;

    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    protected override void Awake()
    {
        base.Awake();

        LoadComponents();
        InitializeAds();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _unityRewardAds = GetComponentInChildren<UnityRewardAds>();
    }

    private void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("--- (UNITY ADS) Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"--- (UNITY ADS) Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void LoadAds()
    {
        _unityRewardAds.LoadAd();
    }

    public void ShowAds()
    {
        _unityRewardAds.ShowAd();
    }
}