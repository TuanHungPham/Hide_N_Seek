using TigerForge;
using UnityEngine;

public class ShopEventHandler : MonoBehaviour
{
    private GameplayManager GameplayManager => GameplayManager.Instance;
    [SerializeField] private long _exchangeTicketQuantity;
    [SerializeField] private long _exchangeCoinQuantity;

    public void ExchangeResources()
    {
        long adsTicketQuantity = GameplayManager.GetTotalTicket();
        if (adsTicketQuantity < _exchangeTicketQuantity) return;

        GameplayManager.AddCoin(_exchangeCoinQuantity);
        GameplayManager.ConsumeTicket(_exchangeTicketQuantity);
    }

    public void EmitClosingShopEvent()
    {
        EventManager.EmitEvent(EventID.CLOSING_SHOP);
    }
}