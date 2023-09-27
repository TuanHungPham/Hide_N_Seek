using TigerForge;
using UnityEngine;

public class ShopEventHandler : MonoBehaviour
{
    public void EmitClosingShopEvent()
    {
        EventManager.EmitEvent(EventID.CLOSING_SHOP);
    }
}