using System.Collections.Generic;
using UnityEngine;

public enum DataType
{
    COSTUME_DATA,
    PET_DATA,
}

public class IngameDataManager : MonoBehaviour
{
    [SerializeField] private CostumeDataManager _costumeDataManager;
    [SerializeField] private PetDataManager _petDataManager;
    [SerializeField] private ResourceDataManager _resourceDataManager;

    public static IngameDataManager Instance { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        HandleSingleton();
        LoadComponents();
    }

    private void HandleSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _costumeDataManager = GetComponentInChildren<CostumeDataManager>();
        _petDataManager = GetComponentInChildren<PetDataManager>();
        _resourceDataManager = GetComponentInChildren<ResourceDataManager>();
    }

    public List<ItemShop> GetItemShopDataList(eShopDataType type)
    {
        switch (type)
        {
            case eShopDataType.PET_SHOP:
                return _petDataManager.GetItemShopList();
            case eShopDataType.COSTUME_SHOP:
                return _costumeDataManager.GetItemShopList();
            default:
                return null;
        }
    }

    public Costume GetCurrentUsingCostume()
    {
        return _costumeDataManager.GetCurrentUsingItem();
    }

    public GameObject GetCurrentUsingPet()
    {
        return _petDataManager.GetCurrentPet();
    }

    public void AddResourceData(eResourceDataType type, long quantity)
    {
        _resourceDataManager.AddData(type, quantity);
    }

    public void SetResourceData(eResourceDataType type, long quantity)
    {
        _resourceDataManager.SetData(type, quantity);
    }

    public long GetResourceData(eResourceDataType type)
    {
        return _resourceDataManager.GetResourceData(type);
    }

    private void SaveData()
    {
        _resourceDataManager.SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}