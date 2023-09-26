using System.Collections.Generic;
using UnityEngine;

public class CostumeDataManager : MonoBehaviour
{
    private static CostumeDataManager instance;
    public static CostumeDataManager Instance => instance;

    [SerializeField] private CostumeData _costumeData;
    [SerializeField] private List<Costume> _costumeDataList = new List<Costume>();

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
        _costumeDataList = _costumeData.CostumeDataList;
    }

    public void SetCostumeOwnedStateData(int costumeID, bool isOwned)
    {
        Costume costume = _costumeDataList.Find((x) => x.GetCostumeID() == costumeID);
        if (costume == null) return;

        costume.SetOwned(isOwned);
    }

    public CostumeData GetCostumeData()
    {
        return _costumeData;
    }

    public List<Costume> GetCostumeDataList()
    {
        return _costumeDataList;
    }
}