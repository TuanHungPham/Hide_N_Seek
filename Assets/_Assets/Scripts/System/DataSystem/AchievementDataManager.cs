using System.Collections.Generic;
using UnityEngine;

public enum eAchievementDataType
{
    NONE,
    LEVEL,
    FOUND_PLAYERS,
    RESCUED_PLAYERS,

    MAX_COUNT_OF_RESOURCE_TYPE,
}

public class AchievementDataManager : MonoBehaviour
{
    private Dictionary<eAchievementDataType, long> _achievementDataDic = new Dictionary<eAchievementDataType, long>();

    private void Awake()
    {
        LoadData();
    }

    private void LoadData()
    {
        int maxCount = (int)eAchievementDataType.MAX_COUNT_OF_RESOURCE_TYPE;

        for (int i = 1; i < maxCount; i++)
        {
            eAchievementDataType dataType = (eAchievementDataType)i;

            string dataString = PlayerPrefs.GetString(dataType.ToString(), "0");
            long data = long.Parse(dataString);

            _achievementDataDic.Add(dataType, data);
        }

        LogSystem.LogDictionary(_achievementDataDic, "ACHIEVEMENT DATA DICTIONARY");
    }

    public void SetData(eAchievementDataType type, long quantity)
    {
        if (!_achievementDataDic.ContainsKey(type)) return;

        _achievementDataDic[type] = quantity;

        LogSystem.LogDictionary(_achievementDataDic, "ACHIEVEMENT DATA DICTIONARY");
    }

    public void AddData(eAchievementDataType type, long quantity)
    {
        if (!_achievementDataDic.ContainsKey(type)) return;

        _achievementDataDic[type] += quantity;
    }

    public long GetAchievementData(eAchievementDataType type)
    {
        return _achievementDataDic[type];
    }

    public Dictionary<eAchievementDataType, long> GetAchievementDataDic()
    {
        return _achievementDataDic;
    }

    public void SaveData()
    {
        foreach (KeyValuePair<eAchievementDataType, long> data in _achievementDataDic)
        {
            PlayerPrefs.SetString(data.Key.ToString(), data.Value.ToString());
        }
    }
}