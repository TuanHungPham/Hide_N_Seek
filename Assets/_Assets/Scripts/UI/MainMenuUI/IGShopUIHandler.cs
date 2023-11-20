using System;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class IGShopUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject _exclamationMark;
    private IngameDataManager IngameDataManager => IngameDataManager.Instance;

    private void Start()
    {
        ListenEvent();
        HandleExclamationMark();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CLOSING_SHOP, HandleExclamationMark);
    }

    private void HandleExclamationMark()
    {
        if (!CheckCanBuyAnything())
        {
            _exclamationMark.gameObject.SetActive(false);
            return;
        }

        _exclamationMark.gameObject.SetActive(true);
    }

    private bool CheckCanBuyAnything()
    {
        long coin = IngameDataManager.GetResourceData(eResourceDataType.COIN);
        List<ItemShop> _costumeShopItemList = IngameDataManager.GetItemShopDataList(eShopDataType.COSTUME_SHOP);
        List<ItemShop> _petShopItemList = IngameDataManager.GetItemShopDataList(eShopDataType.PET_SHOP);

        foreach (var item in _costumeShopItemList)
        {
            if (item.IsOwned() || coin < item.GetItemPrice()) continue;

            return true;
        }

        foreach (var item in _petShopItemList)
        {
            if (item.IsOwned() || coin < item.GetItemPrice()) continue;

            return true;
        }

        return false;
    }
}