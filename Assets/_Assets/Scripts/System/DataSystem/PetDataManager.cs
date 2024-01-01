using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class PetDataManager : MonoBehaviour
{
    [Header(("SO DATA"))] [SerializeField] private PetData _petData;
    [SerializeField] private ItemShopData _itemShopData;

    [Space(20)] [Header("DATA")] [SerializeField]
    private List<ItemShop> _petShopList = new List<ItemShop>();

    [SerializeField] private List<Pet> _petDataList = new List<Pet>();
    [SerializeField] private List<PetBaseData> _petBaseDataList = new List<PetBaseData>();

    [Space(20)] [SerializeField] private Pet _currentUsingPet;

    public List<PetBaseData> PetBaseDataList => _petBaseDataList;

    private void Awake()
    {
        LoadComponents();
        LoadTemplate();
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
        LoadData();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.CHOOSING_ITEM_SHOP, SetData);
    }

    public void LoadTemplate()
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

            PetBaseData petBaseData = new PetBaseData();
            petBaseData.AddData(itemShop.GetItemID(), itemShop.IsSelected(), itemShop.IsOwned());
            AddBaseData(petBaseData);
        }
    }

    public void LoadData()
    {
        if (PetBaseDataList.Count <= 0) return;

        for (int i = 0; i < PetBaseDataList.Count; i++)
        {
            int itemID = PetBaseDataList[i].id;
            bool isSelected = PetBaseDataList[i].isSelected;
            bool isOwned = PetBaseDataList[i].isOwned;

            ItemShop itemShop = _petShopList.Find(x => x.GetItemID() == itemID);
            Pet pet = _petDataList.Find(x => x.GetPetID() == itemID);

            itemShop.SetOwnedState(isOwned);
            itemShop.SetSelectState(isSelected);

            if (isSelected)
            {
                _currentUsingPet = pet;
            }
        }
    }

    public void AddBaseData(PetBaseData petBaseData)
    {
        PetBaseData existedBaseData = PetBaseDataList.Find(x => x.id == petBaseData.id);

        if (existedBaseData == null)
        {
            PetBaseDataList.Add(petBaseData);
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

            PetBaseDataList[i].SetOwnedState(isOwned);
            PetBaseDataList[i].SetSelectionState(isSelected);

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