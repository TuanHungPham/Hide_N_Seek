using UnityEngine;

public class ProductPanel : MonoBehaviour
{
    private GameplayManager GameplayManager => GameplayManager.Instance;

    public void WatchAds()
    {
        UnityAdsManager.Instance.LoadAds();
        UnityAdsManager.Instance.ShowAds();
    }
}