using System;
using System.Collections.Generic;
using UnityEngine;

public class CostumeDataManager : MonoBehaviour
{
    [SerializeField] private CostumeData _costumeData;
    [SerializeField] private List<Costume> _costumeDataList = new List<Costume>();
    [SerializeField] private Costume _currentUsingCostume;

    private void Awake()
    {
        LoadComponents();
        LoadCurrentUsingCostume();
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _costumeDataList = _costumeData.CostumeDataList;
    }

    private void LoadCurrentUsingCostume()
    {
        _currentUsingCostume = _costumeDataList.Find((x) => x.IsSelected());
    }

    public void SetCostumeOwnedStateData(int costumeID, bool isOwned)
    {
        if (!FindCostumeInData(costumeID, out var costume)) return;

        costume.SetOwned(isOwned);
    }

    public void SetCurrentUsingCostume(int costumeID)
    {
        if (!FindCostumeInData(costumeID, out var costume)) return;

        foreach (var costumeData in _costumeDataList)
        {
            costumeData.SetSelectedCostume(false);
        }

        costume.SetSelectedCostume(true);
        _currentUsingCostume = costume;
    }

    private bool FindCostumeInData(int costumeID, out Costume costume)
    {
        costume = _costumeDataList.Find((x) => x.GetCostumeID() == costumeID);
        if (costume != null) return true;
        return false;
    }

    public CostumeData GetCostumeData()
    {
        return _costumeData;
    }

    public List<Costume> GetCostumeDataList()
    {
        return _costumeDataList;
    }

    public Costume GetCurrentUsingCostume()
    {
        return _currentUsingCostume;
    }
}