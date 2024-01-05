using System;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

public class ConfirmationPanelUI : MonoBehaviour
{
    [SerializeField] private GameObject darkScreen;
    [SerializeField] private GameObject panel;
    [SerializeField] private ShopItemUI currentConfirmedShopItemUI;
    [SerializeField] private NotiPanel notiPanel;
    private IngameDataManager IngameDataManager => IngameDataManager.Instance;
    public Action OnBuyingConfirm;

    private void OpenPanel()
    {
        darkScreen.SetActive(true);
        panel.SetActive(true);
    }

    public void InitBuyingConfirmation(ShopItemUI shopItemUI)
    {
        currentConfirmedShopItemUI = shopItemUI;
        OpenPanel();
    }

    public void Confirm()
    {
        if (!CanBuyItem(currentConfirmedShopItemUI))
        {
            var noti = "You don't have enough coin!";
            notiPanel.ShowNotification(noti, eNotiType.ERROR);
            return;
        }

        OnBuyingConfirm?.Invoke();
        currentConfirmedShopItemUI.Buy();

        EmitChoosingItemShopEvent();
        ClosePanel();
    }

    public void ClosePanel()
    {
        darkScreen.SetActive(false);
        panel.SetActive(false);

        currentConfirmedShopItemUI = null;
    }

    private bool CanBuyItem(ShopItemUI shopItemUI)
    {
        long totalCoin = IngameDataManager.GetResourceData(eResourceDataType.COIN);
        int itemPrice = shopItemUI.GetItemPrice();
        return totalCoin >= itemPrice;
    }

    private void EmitChoosingItemShopEvent()
    {
        EventManager.EmitEvent(EventID.CHOOSING_ITEM_SHOP);
    }
}