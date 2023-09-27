using System.Collections.Generic;
using UnityEngine;

public class IngameDataManager : MonoBehaviour
{
    private static IngameDataManager instance;
    public static IngameDataManager Instance => instance;

    [SerializeField] private CostumeDataManager _costumeDataManager;

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
    }

    public List<Costume> GetCostumeDataList()
    {
        return _costumeDataManager.GetCostumeDataList();
    }

    public void SetCostumeOwnedStateData(int costumeID, bool isOwned)
    {
        _costumeDataManager.SetCostumeOwnedStateData(costumeID, isOwned);
    }

    public void SetCurrentUsingCostume(int costumeID)
    {
        _costumeDataManager.SetCurrentUsingCostume(costumeID);
    }

    public Costume GetCurrentUsingCostume()
    {
        return _costumeDataManager.GetCurrentUsingCostume();
    }
}