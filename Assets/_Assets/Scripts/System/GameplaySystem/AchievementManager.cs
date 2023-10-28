using System.Collections.Generic;
using UnityEngine;

public enum eAchievementType
{
    NONE,
    CATCHING_TIME,
    RESCUING_TIME,
    WINNING_TIME,
    COMPLETE_LEVEL_TIME,

    MAX_COUNT,
}

public class AchievementManager : MonoBehaviour
{
    private Dictionary<eAchievementType, long> _ingameAchievementDic = new Dictionary<eAchievementType, long>();
    private IngameDataManager IngameDataManager => IngameDataManager.Instance;

    private void Start()
    {
        InitializeIngameAchievementDic();
    }

    private void InitializeIngameAchievementDic()
    {
        int maxCount = (int)eAchievementType.MAX_COUNT;

        Dictionary<eAchievementType, long> achievementDataDic = IngameDataManager.GetAchievementDataDic();

        for (int i = 1; i < maxCount; i++)
        {
            eAchievementType achievementType = (eAchievementType)i;
            long achievementPoint = achievementDataDic[achievementType];

            _ingameAchievementDic.Add(achievementType, achievementPoint);
        }
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