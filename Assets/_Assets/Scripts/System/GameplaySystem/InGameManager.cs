using System.Collections.Generic;
using UnityEngine;

public class InGameManager : TemporaryMonoSingleton<InGameManager>
{
    [SerializeField] private PetManager _petManager;
    [SerializeField] private CostumeManager _costumeManager;
    [SerializeField] private QuestManager _questManager;
    [SerializeField] private AchievementManager _achievementManager;
    [SerializeField] private UserManager _userManager;

    protected override void Awake()
    {
        base.Awake();
        LoadComponents();
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

    public void SetUsername(string name)
    {
        _userManager.SetUsername(name);
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