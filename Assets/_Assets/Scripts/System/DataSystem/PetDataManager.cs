using System;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

[Serializable]
public class PetBaseData : ShopData
{
    public void AddData(PetBaseData petBaseData)
    {
        id = petBaseData.id;
        isSelected = petBaseData.isSelected;
        isOwned = petBaseData.isOwned;
    }
}

public class PetDataManager : MonoBehaviour
{
    [Header(("SO DATA"))] [SerializeField] private PetData _petData;
    [SerializeField] private ItemShopData _itemShopData;

    [Space(20)] [Header("DATA")] [SerializeField]
    private List<ItemShop> _petShopList = new List<ItemShop>();

    [SerializeField] private List<Pet> _petDataList = new List<Pet>();
    [SerializeField] private List<PetBaseData> _petBaseDataList = new List<PetBaseData>();

    [Space(20)] [SerializeField] private Pet _currentUsingPet;

    private void Awake()
    {
        LoadTemplateListFromSO();
        LoadComponents();
        SetData();
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
        ListenEvent();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CHOOSING_ITEM_SHOP, SetData);
    }

    public void LoadTemplateListFromSO()
    {
        foreach (var data in _petData.PetDataList)
        {
            Pet petData = (Pet)data.Clone();
            _petDataList.Add(petData);
        }

        foreach (var data in _itemShopData.ItemShopList)
        {
            ItemShop itemShop = (ItemShop)data.Clone();
            _petShopList.Add(itemShop);
        }
    }

    public void AddBaseData(PetBaseData petBaseData)
    {
        PetBaseData existedBaseData = _petBaseDataList.Find(x => x.id == petBaseData.id);

        if (existedBaseData == null)
        {
            _petBaseDataList.Add(petBaseData);
        }
        else
        {
            existedBaseData.AddData(petBaseData);
        }
    }

    private void SetData()
    {
        for (int i = 0; i < _petShopList.Count; i++)
        {
            bool isOwned = _petShopList[i].IsOwned();
            bool isSelected = _petShopList[i].IsSelected();

            _petDataList[i].SetOwned(isOwned);
            _petDataList[i].SetSelected(isSelected);

            if (isSelected)
            {
                _currentUsingPet = _petDataList[i];
            }
        }
    }

    public List<ItemShop> GetItemShopList()
    {
        return _petShopList;
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