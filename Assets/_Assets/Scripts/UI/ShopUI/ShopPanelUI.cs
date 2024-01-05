using System;
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

public class ShopPanelUI : MonoBehaviour
{
    [SerializeField] private ShopItemUI _shopItemUIPrefab;
    [SerializeField] private eShopDataType _shopDataType;
    [SerializeField] private List<ShopItemUI> _shopItemUIList = new List<ShopItemUI>();
    [SerializeField] private List<ItemShop> _itemShopList = new List<ItemShop>();
    [SerializeField] private ConfirmationPanelUI confirmationPanelUI;
    private bool _firstInitialization;
    IngameDataManager IngameDataManager => IngameDataManager.Instance;

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
        _firstInitialization = false;
    }

    private void OnEnable()
    {
        ListenEvent();
        InitializeShopItemUI();
    }

    private void ListenEvent()
    {
        confirmationPanelUI.OnBuyingConfirm += DeselectAllShopItem;
    }

    private void RemoveEvent()
    {
        confirmationPanelUI.OnBuyingConfirm -= DeselectAllShopItem;
    }

    private void InitializeShopItemUI()
    {
        Debug.Log("Initializing Shop...");
        _itemShopList = IngameDataManager.GetItemShopDataList(_shopDataType);

        for (int i = 0; i < _itemShopList.Count; i++)
        {
            ShopItemUI shopItemUI;

            if (!_firstInitialization)
            {
                CreateShopItemUI(_shopItemUIPrefab, out shopItemUI);
            }
            else
            {
                shopItemUI = _shopItemUIList[i];
            }

            HandleSettingShopUIData(shopItemUI, _itemShopList[i], _itemShopList[i].IsOwned(), _itemShopList[i].IsSelected());

            _shopItemUIList.Add(shopItemUI);
            shopItemUI.OnClickItemEvent += InteractShopItem;
        }

        _firstInitialization = true;
    }

    private void CreateShopItemUI(ShopItemUI shopItemUI, out ShopItemUI newShopItemUI)
    {
        newShopItemUI = Instantiate(shopItemUI, transform, true);
    }

    private void HandleSettingShopUIData(ShopItemUI shopItemUI, ItemShop itemShop, bool isOwned, bool isSelected)
    {
        shopItemUI.SetUIData(itemShop, isOwned, isSelected);
    }

    private void InteractShopItem(ShopItemUI shopItemUI)
    {
        if (shopItemUI == null) return;

        Debug.Log($"(SHOP) Interacting Shop Item...");

        if (shopItemUI.IsOwned())
        {
            Debug.Log($"(SHOP) Selecting Shop Item...");

            SelectItem(shopItemUI);
            return;
        }

        Debug.Log($"(SHOP) Buying Shop Item...");
        confirmationPanelUI.InitBuyingConfirmation(shopItemUI);
    }

    private void SelectItem(ShopItemUI shopItemUI)
    {
        Debug.Log($"(SHOP) Item Shop Selected...");

        DeselectAllShopItem();

        shopItemUI.Select();

        EmitChoosingItemShopEvent();
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

    private void OnDisable()
    {
        RemoveEvent();
    }
}