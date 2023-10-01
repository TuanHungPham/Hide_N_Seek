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

    private void Awake()
    {
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
    }

    public Costume GetCurrentUsingCostume()
    {
        return _costumeDataManager.GetCurrentUsingItem();
    }

    public GameObject GetCurrentUsingPet()
    {
        return _petDataManager.GetCurrentPet();
    }
}