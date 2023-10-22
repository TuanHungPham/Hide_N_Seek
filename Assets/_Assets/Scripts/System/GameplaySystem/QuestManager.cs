using System;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> _todayQuestList;
    [SerializeField] private SpecialQuest _todaySpecialQuest;
    private InGameManager InGameManager => InGameManager.Instance;
    private IngameDataManager IngameDataManager => IngameDataManager.Instance;

    private void Awake()
    {
        ListenEvent();
    }

    private void Start()
    {
        InitializeTodayQuestList();
    }

    private void ListenEvent()
    {
        EventManager.StartListening(EventID.QUEST_RESETTING, InitializeTodayQuestList);
    }

    public void InitializeTodayQuestList()
    {
        _todayQuestList = IngameDataManager.GetTodayQuestTemplateList();
        _todaySpecialQuest = IngameDataManager.GetTodaySpecialQuest();
    }

    public void UpdateQuestProgress(eQuestType questType, eAchievementType achievementType)
    {
        Quest quest = _todayQuestList.Find(x => x.questType == questType);
        if (quest == null) return;

        float newProgress = quest.currentProgress + InGameManager.GetAchievementPoint(achievementType);

        if (CanUpdateQuest(quest, newProgress)) return;

        quest.currentProgress = newProgress;
        CheckQuestState(quest);
    }

    private void UpdateSpecialQuestProgress()
    {
        if (_todaySpecialQuest.isCompleted) return;

        _todaySpecialQuest.currentProgress++;
        CheckQuestState(_todaySpecialQuest);
    }

    private static bool CanUpdateQuest(Quest quest, float newProgress)
    {
        if (quest.isCompleted) return false;
        if (quest.currentProgress >= quest.targetProgress) return true;
        if (newProgress <= quest.targetProgress) return true;

        return false;
    }

    public void FinishQuest(int questID)
    {
        Quest quest = _todayQuestList.Find(x => x.questID == questID);
        if (quest == null) return;

        quest.FinishQuest();
        UpdateSpecialQuestProgress();
        EmitUpdatingQuestEvent();
    }

    public List<Quest> GetTodayQuestList()
    {
        return _todayQuestList;
    }

    public SpecialQuest GetTodaySpecialQuest()
    {
        return _todaySpecialQuest;
    }

    private void CheckQuestState(Quest quest)
    {
        if (quest.currentProgress >= quest.targetProgress)
        {
            quest.isCompleted = true;
            UpdateSpecialQuestProgress();
        }
        else
        {
            quest.isCompleted = false;
        }

        EmitUpdatingQuestEvent();
    }

    private void CheckQuestState(SpecialQuest specialQuest)
    {
        if (specialQuest.currentProgress >= specialQuest.targetProgress)
        {
            specialQuest.isCompleted = true;
        }
        else
        {
            specialQuest.isCompleted = false;
        }
    }

    private void EmitUpdatingQuestEvent()
    {
        EventManager.EmitEvent(EventID.QUEST_UPDATING);
    }
}