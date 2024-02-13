using System.Collections.Generic;
using UnityEngine;

public enum eAchievementType
{
    NONE,
    CATCHING_TIME,
    RESCUING_TIME,
    WINNING_TIME,
    COMPLETE_LEVEL_TIME,

    MAX_COUNT
}

public class AchievementManager : MonoBehaviour
{
    private readonly Dictionary<eAchievementType, long> _ingameAchievementDic = new();
    private PlayfabManager PlayfabManager => PlayfabManager.Instance;

    private void Start()
    {
        InitializeIngameAchievementDic();
        UpdatePlayerStatistic();
    }

    private void InitializeIngameAchievementDic()
    {
        var maxCount = (int)eAchievementType.MAX_COUNT;

        for (var i = 1; i < maxCount; i++)
        {
            var achievementType = (eAchievementType)i;
            var achievementPoint = 0;

            _ingameAchievementDic.Add(achievementType, achievementPoint);
        }
    }

    private void UpdatePlayerStatistic()
    {
        PlayfabManager.UpdatePlayerStatistic();
    }

    public void AddAchievementPointData(eAchievementType type)
    {
        Debug.Log($"(ACHIEVEMENT) Adding {type} achievement Data Point");
        IngameDataManager.Instance.AddAchievementData(type);
    }

    public void AddAchievementPoint(eAchievementType type)
    {
        Debug.Log($"(ACHIEVEMENT) Adding {type} achievement Point");
        _ingameAchievementDic[type]++;
        AddAchievementPointData(type);
    }

    public float GetAchievementPoint(eAchievementType type)
    {
        return _ingameAchievementDic[type];
    }
}