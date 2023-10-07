using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class ShopItemHandler : MonoBehaviour
{
    [SerializeField] private ItemShopData itemShopData;
    [SerializeField] private List<ShopItemUI> _shopItemUIList = new List<ShopItemUI>();
    [SerializeField] private ShopItemUI _shopItemUIPrefab;

    private void Awake()
    {
        LoadComponents();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
    }

    private void Start()
    {
        InitializeShopItemUI();
    }

    private void InitializeShopItemUI()
    {
        Debug.Log("Initializing Shop...");
        List<ItemShop> shopItemUIList = itemShopData.ItemShopList;

        for (int i = 0; i < shopItemUIList.Count; i++)
        {
            ShopItemUI shopItemUI;

            CreateShopItemUI(_shopItemUIPrefab, out shopItemUI);
            HandleSettingShopUIData(shopItemUI, shopItemUIList[i], shopItemUIList[i].IsOwned(), shopItemUIList[i].IsSelected());

            _shopItemUIList.Add(shopItemUI);
            shopItemUI.OnClickItemEvent += InteractShopItem;
        }
    }

    private void CreateShopItemUI(ShopItemUI shopItemUI, out ShopItemUI newShopItemUI)
    {
        newShopItemUI = Instantiate(shopItemUI);
        newShopItemUI.transform.SetParent(transform, true);
    }

    private void HandleSettingShopUIData(ShopItemUI shopItemUI, ItemShop itemShop, bool isOwned, bool isSelected)
    {
        shopItemUI.SetUIData(itemShop, isOwned, isSelected);
    }

    private void InteractShopItem(ShopItemUI shopItemUI)
    {
        if (shopItemUI == null) return;

        if (shopItemUI.IsOwned())
        {
            SelectItem(shopItemUI);
            return;
        }

        BuyItem(shopItemUI);
    }

    private void SelectItem(ShopItemUI shopItemUI)
    {
        DeselectAllShopItem();

        shopItemUI.Select();

        EmitChoosingItemShopEvent();
    }

    private void BuyItem(ShopItemUI shopItemUI)
    {
        if (CanBuyItem(shopItemUI)) return;

        DeselectAllShopItem();

        shopItemUI.Buy();

        EmitChoosingItemShopEvent();
    }

    private static bool CanBuyItem(ShopItemUI shopItemUI)
    {
        long totalCoin = IngameDataManager.Instance.GetResourceData(eResourceDataType.COIN);
        int itemPrice = shopItemUI.GetItemPrice();
        if (totalCoin < itemPrice) return true;
        return false;
    }

    private void DeselectAllShopItem()
    {
        foreach (var shopItemUI in _shopItemUIList)
        {
            shopItemUI.Deselect();
        }
    }

    private void EmitChoosingItemShopEvent()
    {
        EventManager.EmitEvent(EventID.CHOOSING_ITEM_SHOP);
    }
}