using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class CostumeDataManager : MonoBehaviour
{
    [Header(("SO DATA"))] [SerializeField] private CostumeData _costumeData;
    [SerializeField] private ItemShopData _itemShopData;

    [Space(20)] [Header("DATA")] [SerializeField]
    private List<ItemShop> _costumeShopList = new List<ItemShop>();

    [SerializeField] private List<Costume> _costumeDataList = new List<Costume>();
    [SerializeField] private List<CostumeBaseData> _costumeBaseDataList = new List<CostumeBaseData>();

    [Space(20)] [SerializeField] private Costume _currentUsingCostume;

    public List<CostumeBaseData> CostumeBaseDataList => _costumeBaseDataList;

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
        foreach (var data in _costumeData.CostumeDataList)
        {
            Costume costumeData = (Costume)data.Clone();
            _costumeDataList.Add(costumeData);
        }

        foreach (var data in _itemShopData.ItemShopList)
        {
            ItemShop itemShop = (ItemShop)data.Clone();
            _costumeShopList.Add(itemShop);

            CostumeBaseData costumeBaseData = new CostumeBaseData();
            costumeBaseData.AddData(itemShop.GetItemID(), itemShop.IsSelected(), itemShop.IsOwned());
            AddBaseData(costumeBaseData);
        }
    }

    public void LoadData()
    {
        if (CostumeBaseDataList.Count <= 0) return;

        for (int i = 0; i < CostumeBaseDataList.Count; i++)
        {
            int itemID = CostumeBaseDataList[i].id;
            bool isSelected = CostumeBaseDataList[i].isSelected;
            bool isOwned = CostumeBaseDataList[i].isOwned;

            ItemShop itemShop = _costumeShopList.Find(x => x.GetItemID() == itemID);
            Costume costume = _costumeDataList.Find(x => x.GetCostumeID() == itemID);

            itemShop.SetOwnedState(isOwned);
            itemShop.SetSelectState(isSelected);

            if (isSelected)
            {
                _currentUsingCostume = costume;
            }
        }
    }

    public void AddBaseData(CostumeBaseData costumeBaseData)
    {
        CostumeBaseData existedBaseData = CostumeBaseDataList.Find(x => x.id == costumeBaseData.id);

        if (existedBaseData == null)
        {
            CostumeBaseDataList.Add(costumeBaseData);
        }
        else
        {
            existedBaseData.AddData(costumeBaseData);
        }
    }

    private void SetData()
    {
        for (int i = 0; i < _costumeShopList.Count; i++)
        {
            bool isOwned = _costumeShopList[i].IsOwned();
            bool isSelected = _costumeShopList[i].IsSelected();

            _costumeBaseDataList[i].SetOwnedState(isOwned);
            _costumeBaseDataList[i].SetSelectionState(isSelected);

            if (isSelected)
            {
                _currentUsingCostume = _costumeDataList[i];
            }
        }
    }

    public List<ItemShop> GetItemShopList()
    {
        return _costumeShopList;
    }

    public ItemShop GetCurrentSelectedCostumeItemShop()
    {
        return _costumeShopList.Find(x => x.GetItemID() == _currentUsingCostume.GetCostumeID());
    }

    public Costume GetCurrentUsingItem()
    {
        return _currentUsingCostume;
    }
}