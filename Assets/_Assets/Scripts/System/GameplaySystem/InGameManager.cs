using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    private static InGameManager instance;
    public static InGameManager Instance => instance;

    [SerializeField] private PetManager _petManager;
    [SerializeField] private CostumeManager _costumeManager;
    [SerializeField] private QuestManager _questManager;
    [SerializeField] private AchievementManager _achievementManager;
    [SerializeField] private UserManager _userManager;

    private void Awake()
    {
        HandleInstanceObject();
        LoadComponents();
    }

    private void HandleInstanceObject()
    {
        instance = this;
    }

    private void Reset()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _petManager = GetComponentInChildren<PetManager>();
        _costumeManager = GetComponentInChildren<CostumeManager>();
        _questManager = GetComponentInChildren<QuestManager>();
        _achievementManager = GetComponentInChildren<AchievementManager>();
        _userManager = GetComponentInChildren<UserManager>();
    }

    public void UpdateQuestProgress(eQuestType questType, eAchievementType achievementType)
    {
        _questManager.UpdateQuestProgress(questType, achievementType);
    }

    public void FinishQuest(int questID)
    {
        _questManager.FinishQuest(questID);
    }

    public List<Quest> GetTodayQuestList()
    {
        return _questManager.GetTodayQuestList();
    }

    public SpecialQuest GetTodaySpecialQuest()
    {
        return _questManager.GetTodaySpecialQuest();
    }

    public void AddAchievementPoint(eAchievementType type)
    {
        _achievementManager.AddAchievementPoint(type);
    }

    public void AddAchievementPointData(eAchievementDataType type)
    {
        _achievementManager.AddAchievementPointData(type);
    }

    public void SetUsername(string name)
    {
        _userManager.SetUsername(name);
    }

    public long GetAchievementPointData(eAchievementDataType type)
    {
        return _achievementManager.GetAchievementPointData(type);
    }

    public float GetAchievementPoint(eAchievementType type)
    {
        return _achievementManager.GetAchievementPoint(type);
    }

    public string GetUsername()
    {
        return _userManager.GetUsername();
    }
}