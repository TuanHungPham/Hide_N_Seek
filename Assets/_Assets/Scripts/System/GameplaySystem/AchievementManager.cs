using System.Collections.Generic;
using UnityEngine;

public enum eAchievementType
{
    CATCHING_TIME,
    RESCUING_TIME,
    WINNING_TIME,
    COMPLETE_LEVEL_TIME,

    MAX_COUNT,
}

public class AchievementManager : MonoBehaviour
{
    private Dictionary<eAchievementType, float> _ingameAchievementDic = new Dictionary<eAchievementType, float>();
    private Dictionary<eAchievementDataType, long> _achievementDic = new Dictionary<eAchievementDataType, long>();
    private IngameDataManager IngameDataManager => IngameDataManager.Instance;

    private void Start()
    {
        InitializeAchievementDic();
        InitializeIngameAchievementDic();
    }

    private void InitializeIngameAchievementDic()
    {
        int maxCount = (int)eAchievementType.MAX_COUNT;
        for (int i = 0; i < maxCount; i++)
        {
            eAchievementType achievementType = (eAchievementType)i;
            float achievementPoint = 0;

            _ingameAchievementDic.Add(achievementType, achievementPoint);
        }
    }

    private void InitializeAchievementDic()
    {
        _achievementDic = IngameDataManager.GetAchievementDataDic();
    }

    public void AddAchievementPointData(eAchievementDataType type)
    {
        Debug.Log($"(ACHIEVEMENT) Adding {type} achievement Data Point");
        _achievementDic[type]++;
    }

    public void AddAchievementPoint(eAchievementType type)
    {
        Debug.Log($"(ACHIEVEMENT) Adding {type} achievement Point");
        _ingameAchievementDic[type]++;
    }

    public float GetAchievementPoint(eAchievementType type)
    {
        return _ingameAchievementDic[type];
    }

    public long GetAchievementPointData(eAchievementDataType type)
    {
        return _achievementDic[type];
    }
}