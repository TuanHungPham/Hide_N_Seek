using System.Collections.Generic;
using UnityEngine;

public class ShopItemUIPanel : MonoBehaviour
{
    [SerializeField] private CostumeShopData _costumeShopData;
    [SerializeField] private List<ShopItemUI> _shopItemUIList = new List<ShopItemUI>();
    [SerializeField] private ShopItemUI _shopItemUIPrefab;
    [SerializeField] private CostumeShop _currentSelectedCostume;
    private CostumeDataManager CostumeDataManager => CostumeDataManager.Instance;

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
        List<CostumeShop> shopItemUIList = _costumeShopData.CostumeShopList;
        List<Costume> costumeDataList = CostumeDataManager.GetCostumeDataList();

        for (int i = 0; i < shopItemUIList.Count; i++)
        {
            ShopItemUI shopItemUI;

            CreateShopItemUI(_shopItemUIPrefab, out shopItemUI);
            HandleSettingShopUIData(shopItemUI, shopItemUIList[i], costumeDataList[i].IsOwned());

            _shopItemUIList.Add(shopItemUI);
            shopItemUI.OnClickItemEvent += InteractShopItem;
        }
    }

    private void CreateShopItemUI(ShopItemUI shopItemUI, out ShopItemUI newShopItemUI)
    {
        newShopItemUI = Instantiate(shopItemUI);
        newShopItemUI.transform.SetParent(transform, true);
    }

    private void HandleSettingShopUIData(ShopItemUI shopItemUI, CostumeShop costumeShop, bool isOwned)
    {
        shopItemUI.SetUIData(costumeShop, isOwned);
    }

    private void InteractShopItem(ShopItemUI shopItemUI)
    {
        if (shopItemUI == null) return;

        if (shopItemUI.IsOwned())
        {
            DeselectAllShopItem();
            shopItemUI.Select();
            _currentSelectedCostume = shopItemUI.GetCostumeShop();
            return;
        }

        DeselectAllShopItem();
        shopItemUI.Buy();
    }

    private void DeselectAllShopItem()
    {
        foreach (var shopItemUI in _shopItemUIList)
        {
            shopItemUI.Deselect();
        }
    }
}