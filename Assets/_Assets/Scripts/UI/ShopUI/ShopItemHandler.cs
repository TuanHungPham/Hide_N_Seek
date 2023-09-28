using System.Collections.Generic;
using UnityEngine;
using TigerForge;

public class ShopItemHandler : MonoBehaviour
{
    [SerializeField] private ItemShopData itemShopData;
    [SerializeField] private List<ShopItemUI> _shopItemUIList = new List<ShopItemUI>();
    [SerializeField] private ShopItemUI _shopItemUIPrefab;
    [SerializeField] private ItemShop currentSelectedItem;
    private IngameDataManager IngameDataManager => IngameDataManager.Instance;

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
        List<Costume> costumeDataList = IngameDataManager.GetCostumeDataList();

        for (int i = 0; i < shopItemUIList.Count; i++)
        {
            ShopItemUI shopItemUI;

            CreateShopItemUI(_shopItemUIPrefab, out shopItemUI);
            HandleSettingShopUIData(shopItemUI, shopItemUIList[i], costumeDataList[i].IsOwned());
            HandleSettingShopUIData(shopItemUI, shopItemUIList[i], false);

            _shopItemUIList.Add(shopItemUI);
            shopItemUI.OnClickItemEvent += InteractShopItem;
        }
    }

    private void CreateShopItemUI(ShopItemUI shopItemUI, out ShopItemUI newShopItemUI)
    {
        newShopItemUI = Instantiate(shopItemUI);
        newShopItemUI.transform.SetParent(transform, true);
    }

    private void HandleSettingShopUIData(ShopItemUI shopItemUI, ItemShop itemShop, bool isOwned)
    {
        shopItemUI.SetUIData(itemShop, isOwned);
    }

    private void InteractShopItem(ShopItemUI shopItemUI)
    {
        if (shopItemUI == null) return;

        if (shopItemUI.IsOwned())
        {
            DeselectAllShopItem();
            SelectItem(shopItemUI);
            currentSelectedItem = shopItemUI.GetCostumeShop();
            return;
        }

        DeselectAllShopItem();
        BuyItem(shopItemUI);
    }

    private void SelectItem(ShopItemUI shopItemUI)
    {
        int itemID = shopItemUI.GetItemID();
        shopItemUI.Select();
        IngameDataManager.SetCurrentUsingCostume(itemID);
    }

    private void BuyItem(ShopItemUI shopItemUI)
    {
        int itemID = shopItemUI.GetItemID();
        shopItemUI.Buy();
        IngameDataManager.SetCostumeOwnedStateData(itemID, true);
    }

    private void DeselectAllShopItem()
    {
        foreach (var shopItemUI in _shopItemUIList)
        {
            shopItemUI.Deselect();
        }
    }
}