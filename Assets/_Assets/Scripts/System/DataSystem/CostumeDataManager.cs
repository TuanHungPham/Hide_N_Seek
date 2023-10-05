using System;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class CostumeDataManager : MonoBehaviour
{
    [SerializeField] private CostumeData _costumeData;
    [SerializeField] private ItemShopData _itemShopData;
    [SerializeField] private List<Costume> _costumeDataList = new List<Costume>();
    [SerializeField] private Costume _currentUsingCostume;

    private void Awake()
    {
        LoadComponents();
        // LoadCurrentUsingCostume();
        SetData();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _costumeDataList = _costumeData.CostumeDataList;
    }

    private void Start()
    {
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CHOOSING_ITEM_SHOP, SetData);
    }

    private void SetData()
    {
        int selectedItemID = 0;
        List<ItemShop> itemShopList = _itemShopData.ItemShopList;

        for (int i = 0; i < itemShopList.Count; i++)
        {
            bool isOwned = itemShopList[i].IsOwned();
            bool isSelected = itemShopList[i].IsSelected();

            _costumeDataList[i].SetOwned(isOwned);
            _costumeDataList[i].SetSelected(isSelected);

            if (isSelected)
            {
                _currentUsingCostume = _costumeDataList[i];
            }
        }
    }

    public Costume GetCurrentUsingItem()
    {
        return _currentUsingCostume;
    }
}