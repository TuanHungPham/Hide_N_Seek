using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public enum eShopDataType
{
    NONE,
    PET_SHOP,
    COSTUME_SHOP,

    MAX_SHOP_DATA_TYPE_COUNT
}

public class ShopItemHandler : MonoBehaviour
{
    [SerializeField] private ShopItemUI _shopItemUIPrefab;
    [SerializeField] private eShopDataType _shopDataType;
    [SerializeField] private List<ShopItemUI> _shopItemUIList = new List<ShopItemUI>();
    [SerializeField] private List<ItemShop> _itemShopList = new List<ItemShop>();

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
        _itemShopList = IngameDataManager.Instance.GetItemShopDataList(_shopDataType);

        for (int i = 0; i < _itemShopList.Count; i++)
        {
            ShopItemUI shopItemUI;

            CreateShopItemUI(_shopItemUIPrefab, out shopItemUI);
            HandleSettingShopUIData(shopItemUI, _itemShopList[i], _itemShopList[i].IsOwned(), _itemShopList[i].IsSelected());

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