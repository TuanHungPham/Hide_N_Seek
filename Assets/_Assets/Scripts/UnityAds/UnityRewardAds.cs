using UnityEngine;
using UnityEngine.Advertisements;

public class UnityRewardAds : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener
{
    [Header("Initializing Reward Ads Setting")] [SerializeField]
    string _androidAdUnitId = "Rewarded_Android";

    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";

    [Space(10)] [Header("Reward")] [SerializeField]
    private long _ticketRewardQuantity;

    string _adUnitId = null;
    private GameplayManager GameplayManager => GameplayManager.Instance;

    void Awake()
    {
        InitializeRewardAds();
    }

    private void InitializeRewardAds()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
    }

    public void LoadAd()
    {
        Debug.Log("--- (UNITY ADS) Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("--- (UNITY ADS) Ad Loaded: " + adUnitId);
    }

    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("--- (UNITY ADS) Unity Ads Rewarded Ad Completed");
            GetReward();
        }
    }

    private void GetReward()
    {
        GameplayManager.AddTicket(_ticketRewardQuantity);
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"--- (UNITY ADS) Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"--- (UNITY ADS) Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId)
    {
    }

    public void OnUnityAdsShowClick(string adUnitId)
    {
    }
}