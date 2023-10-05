using System;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class PetDataManager : MonoBehaviour
{
    [SerializeField] private PetData _petData;
    [SerializeField] private ItemShopData _itemShopData;
    [SerializeField] private List<Pet> _petDataList = new List<Pet>();
    [SerializeField] private Pet _currentUsingPet;

    private void Awake()
    {
        LoadComponents();
        ListenEvent();
        SetData();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _petDataList = _petData.PetDataList;
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

            _petDataList[i].SetOwned(isOwned);
            _petDataList[i].SetSelected(isSelected);

            if (isSelected)
            {
                _currentUsingPet = _petDataList[i];
            }
        }
    }

    public Pet GetCurrentUsingItem()
    {
        return _currentUsingPet;
    }

    public GameObject GetCurrentPet()
    {
        return _currentUsingPet.GetPet();
    }
}