using System;
using System.Collections.Generic;
using UnityEngine;

public enum eResourceDataType
{
    NONE,
    COIN,
    ADS_TICKET,

    MAX_COUNT_OF_RESOURCE_TYPE,
}

public class ResourceDataManager : MonoBehaviour
{
    [SerializeField] private long _coin;
    [SerializeField] private long _adsTicket;
    private Dictionary<eResourceDataType, long> _resourcesDataDic = new Dictionary<eResourceDataType, long>();

    private void Awake()
    {
        LoadData();
    }

    private void LoadData()
    {
        int maxCount = (int)eResourceDataType.MAX_COUNT_OF_RESOURCE_TYPE;

        for (int i = 1; i < maxCount; i++)
        {
            eResourceDataType dataType = (eResourceDataType)i;

            string dataString = PlayerPrefs.GetString(dataType.ToString(), "0");
            long data = long.Parse(dataString);

            _resourcesDataDic.Add(dataType, data);
        }

        LogSystem.LogDictionary(_resourcesDataDic);
    }

    public void SetData(eResourceDataType type, long quantity)
    {
        if (!_resourcesDataDic.ContainsKey(type)) return;

        _resourcesDataDic[type] = quantity;
    }

    public void AddData(eResourceDataType type, long quantity)
    {
        if (!_resourcesDataDic.ContainsKey(type)) return;

        _resourcesDataDic[type] += quantity;
    }

    public long GetResourceData(eResourceDataType type)
    {
        return _resourcesDataDic[type];
    }

    public void SaveData()
    {
        foreach (KeyValuePair<eResourceDataType, long> data in _resourcesDataDic)
        {
            PlayerPrefs.SetString(data.Key.ToString(), data.Value.ToString());
        }
    }
}