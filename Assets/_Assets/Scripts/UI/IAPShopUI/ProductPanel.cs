using UnityEngine;

public class ProductPanel : MonoBehaviour
{
    private GameplayManager GameplayManager => GameplayManager.Instance;
    [SerializeField] private long _exchangeTicketQuantity;
    [SerializeField] private long _exchangeCoinQuantity;

    public void WatchAds()
    {
        UnityAdsManager.Instance.LoadAds();
        UnityAdsManager.Instance.ShowAds();
    }

    public void ExchangeResources()
    {
        long adsTicketQuantity = GameplayManager.GetTotalTicket();
        if (adsTicketQuantity < _exchangeTicketQuantity) return;

        GameplayManager.AddCoin(_exchangeCoinQuantity);
        GameplayManager.ConsumeTicket(_exchangeTicketQuantity);
    }
}