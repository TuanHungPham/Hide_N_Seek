using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eAchievementType
{
    CATCHING_TIME,
    RESCUING_TIME,

    MAX_COUNT,
}

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private List<float> _achivementPointList = new List<float>();
    private Dictionary<eAchievementType, float> _ingameAchievementDic = new Dictionary<eAchievementType, float>();

    private void Start()
    {
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

    public void AddAchievementPoint(eAchievementType type)
    {
        Debug.Log($"(ACHIEVEMENT) Adding {type} achievement Point");
        _ingameAchievementDic[type]++;
    }

    public float GetAchievementPoint(eAchievementType type)
    {
        return _ingameAchievementDic[type];
    }
}