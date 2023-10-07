using System;
using System.Collections.Generic;
using UnityEngine;

public enum DataType
{
    COSTUME_DATA,
    PET_DATA,
}

public class IngameDataManager : MonoBehaviour
{
    private static IngameDataManager instance;
    public static IngameDataManager Instance => instance;

    [SerializeField] private CostumeDataManager _costumeDataManager;
    [SerializeField] private PetDataManager _petDataManager;
    [SerializeField] private ResourceDataManager _resourceDataManager;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
        LoadComponents();
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