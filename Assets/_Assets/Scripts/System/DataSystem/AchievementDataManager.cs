using System.Collections.Generic;
using UnityEngine;

public class AchievementDataManager : MonoBehaviour
{
    private readonly Dictionary<eAchievementType, long> _achievementDataDic = new();

    public Dictionary<eAchievementType, AchievementBaseData> AchievementBaseDataDic { get; } = new();

    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        var maxCount = (int)eAchievementType.MAX_COUNT;

        for (var i = 1; i < maxCount; i++)
        {
            var dataType = (eAchievementType)i;

            long data = 0;
            if (!AchievementBaseDataDic.ContainsKey(dataType))
            {
                var achievementBaseData = new AchievementBaseData();
                achievementBaseData.AddData(data);

                AchievementBaseDataDic.Add(dataType, achievementBaseData);
            }
            else
            {
                data = AchievementBaseDataDic[dataType].achievementData;
            }

            _achievementDataDic[dataType] = data;
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
            var achievementBaseData = new AchievementBaseData();
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
            AchievementBaseDataDic.Add(type, achievementBaseData);
        else
            AchievementBaseDataDic[type].AddData(achievementBaseData.achievementData);
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
        foreach (var data in _achievementDataDic) PlayerPrefs.SetString(data.Key.ToString(), data.Value.ToString());
    }
}