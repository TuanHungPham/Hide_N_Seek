using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementDataManager : MonoBehaviour
{
    private Dictionary<eAchievementType, long> _achievementDataDic = new Dictionary<eAchievementType, long>();
    private Dictionary<eAchievementType, AchievementBaseData> _achievementBaseDataDic = new Dictionary<eAchievementType, AchievementBaseData>();

    public Dictionary<eAchievementType, AchievementBaseData> AchievementBaseDataDic => _achievementBaseDataDic;

    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        int maxCount = (int)eAchievementType.MAX_COUNT;

        for (int i = 1; i < maxCount; i++)
        {
            eAchievementType dataType = (eAchievementType)i;

            long data = 0;
            if (!AchievementBaseDataDic.ContainsKey(dataType))
            {
                AchievementBaseData achievementBaseData = new AchievementBaseData();
                achievementBaseData.AddData(data);

                AchievementBaseDataDic.Add(dataType, achievementBaseData);
            }
            else
            {
                data = AchievementBaseDataDic[dataType].achievementData;
            }

            _achievementDataDic.TryAdd(dataType, data);
        }

        LogSystem.LogDictionary(_achievementDataDic, "ACHIEVEMENT DATA DICTIONARY");
    }

    public void SetData(eAchievementType type, long quantity)
    {
        if (!_achievementDataDic.ContainsKey(type)) return;

        _achievementDataDic[type] = quantity;
        AddBaseData(type, quantity);

        LogSystem.LogDictionary(_achievementDataDic, "ACHIEVEMENT DATA DICTIONARY");
    }

    public void AddData(eAchievementType type)
    {
        if (!_achievementDataDic.ContainsKey(type)) return;

        _achievementDataDic[type]++;
        AddBaseData(type, _achievementDataDic[type]);
    }

    public void AddBaseData(eAchievementType type, long quantity)
    {
        if (!AchievementBaseDataDic.ContainsKey(type))
        {
            AchievementBaseData achievementBaseData = new AchievementBaseData();
            achievementBaseData.AddData(quantity);

            AchievementBaseDataDic.Add(type, achievementBaseData);
        }
        else
        {
            AchievementBaseDataDic[type].AddData(quantity);
        }
    }

    public void AddBaseData(eAchievementType type, AchievementBaseData achievementBaseData)
    {
        if (!AchievementBaseDataDic.ContainsKey(type))
        {
            AchievementBaseDataDic.Add(type, achievementBaseData);
        }
        else
        {
            AchievementBaseDataDic[type].AddData(achievementBaseData.achievementData);
        }
    }

    public long GetAchievementData(eAchievementType type)
    {
        return _achievementDataDic[type];
    }

    public Dictionary<eAchievementType, long> GetAchievementDataDic()
    {
        return _achievementDataDic;
    }

    public void SaveData()
    {
        foreach (KeyValuePair<eAchievementType, long> data in _achievementDataDic)
        {
            PlayerPrefs.SetString(data.Key.ToString(), data.Value.ToString());
        }
    }
}