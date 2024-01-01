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
    private Dictionary<eResourceDataType, long> _resourcesDataDic = new Dictionary<eResourceDataType, long>();
    private Dictionary<eResourceDataType, ResourceBaseData> _resourcesBaseDataDic = new Dictionary<eResourceDataType, ResourceBaseData>();

    public Dictionary<eResourceDataType, ResourceBaseData> ResourcesBaseDataDic => _resourcesBaseDataDic;

    private void Awake()
    {
        InitializeNewData();
    }

    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        foreach (var baseData in ResourcesBaseDataDic)
        {
            var eResourceDataType = baseData.Key;
            var valueResourceData = baseData.Value.resourceData;

            if (!_resourcesDataDic.ContainsKey(eResourceDataType))
            {
                _resourcesDataDic.Add(eResourceDataType, valueResourceData);
                return;
            }

            _resourcesDataDic[eResourceDataType] = valueResourceData;
        }
    }

    private void InitializeNewData()
    {
        int maxCount = (int)eResourceDataType.MAX_COUNT_OF_RESOURCE_TYPE;

        for (int i = 1; i < maxCount; i++)
        {
            eResourceDataType dataType = (eResourceDataType)i;

            _resourcesDataDic.Add(dataType, 0);
        }

        LogSystem.LogDictionary(_resourcesDataDic);
    }

    public void SetData(eResourceDataType type, long quantity)
    {
        if (!_resourcesDataDic.ContainsKey(type)) return;

        _resourcesDataDic[type] = quantity;
        AddBaseData(type, quantity);
    }

    public void AddData(eResourceDataType type, long quantity)
    {
        if (!_resourcesDataDic.ContainsKey(type)) return;

        _resourcesDataDic[type] += quantity;
        AddBaseData(type, _resourcesDataDic[type]);
    }

    public void AddBaseData(eResourceDataType type, ResourceBaseData resourceBaseData)
    {
        if (!ResourcesBaseDataDic.ContainsKey(type))
        {
            ResourcesBaseDataDic.Add(type, resourceBaseData);
            return;
        }

        ResourcesBaseDataDic[type] = resourceBaseData;
    }

    public void AddBaseData(eResourceDataType type, long quantity)
    {
        if (ResourcesBaseDataDic.ContainsKey(type))
        {
            ResourcesBaseDataDic[type].AddData(quantity);
            return;
        }

        ResourceBaseData resourceBaseData = new ResourceBaseData();
        resourceBaseData.resourceData = quantity;

        AddBaseData(type, resourceBaseData);
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