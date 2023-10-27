using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public enum eResourceDataType
{
    NONE,
    COIN,
    ADS_TICKET,

    MAX_COUNT_OF_RESOURCE_TYPE,
}

public class ResourceBaseData : BaseData
{
    public long resourceData;

    public void AddData(long resourceData)
    {
        this.resourceData = resourceData;
    }

    public override void ParseToData(string json)
    {
        ResourceBaseData resourceBaseData = JsonConvert.DeserializeObject<ResourceBaseData>(json);
        AddData(resourceBaseData.resourceData);
    }
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

    private void LoadData()
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
        ResourcesBaseDataDic.Add(type, resourceBaseData);
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